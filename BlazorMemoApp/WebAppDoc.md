**📘 MemoWebApp Documentation**

**1\. 🧭 System Overview**

**Purpose**:  
MemoWebApp is designed to manage garment purchase memos, linking buyers, styles, suppliers, and PO details with itemized breakdowns from external APIs. It supports real-time calculations, stock tracking, and surcharge auditing.

**Tech Stack**:

- Backend: .NET Core 9 (ASP.NET MVC/Web API)
- Frontend: Blazor
- Database: Microsoft SQL Server
- External APIs:
    - PO List: GET http://192.168.0.6:8000/Api/GetPoes?pono={query}
    - PO Detail: GET http://192.168.0.6:8000/Api/GetPoH2HDetail?pono={query}

**2\. 🗃️ Database Schema**

**📌 Table: MemoHeader**

|     |     |     |
| --- | --- | --- |
| **Column** | **Type** | **Description** |
| ID  | INT (PK) | Unique identifier |
| BuyerID | INT (FK) | References Buyer table |
| MemoNo | VARCHAR(20) | Auto-generated, format: MO000001 |
| MemoDate | DATE | Selected from pickup date |
| StyleID | INT (FK) | References Style table |
| GmtQty | INT | Garment quantity |
| GmtFobValue | DECIMAL(18,2) | FOB value |
| SupplierID | INT (FK) | References Supplier table |
| PONumber | VARCHAR(50) | Retrieved via PO API |

**📌 Table: MemoDetail**

|     |     |     |
| --- | --- | --- |
| **Column** | **Type** | **Description** |
| ID  | INT (PK) | Unique identifier |
| HeaderID | INT (FK) | References MemoHeader |
| Article | VARCHAR(50) | From PO Detail API |
| Color | VARCHAR(30) | From PO Detail API |
| Size | VARCHAR(10) | From PO Detail API |
| Price | DECIMAL(18,2) | From PO Detail API |
| Unit | VARCHAR(10) | From PO Detail API |
| BOMQty | INT | User input |
| BOMAmount | DECIMAL(18,2) | \= Price × BOMQty |
| AvailStockQty | INT | User input |
| StockAmount | DECIMAL(18,2) | \= Price × AvailStockQty |
| PurchaseQty | INT | \= BOMQty − AvailStockQty |
| PurchaseAmount | DECIMAL(18,2) | \= Price × PurchaseQty |
| MCQQty | INT | User input |
| MCQAmount | DECIMAL(18,2) | \= Price × MCQQty |
| Diff | DECIMAL(18,2) | \= PurchaseAmount − MCQAmount |
| SurchargePaid | DECIMAL(18,2) | User input |
| StockFromMCQ | INT | \= MCQQty − PurchaseQty |
| StockUsableQty | INT | User input |
| StockUsableAmount | DECIMAL(18,2) | \= Price × StockUsableQty |
| StockNonUsableQty | INT | \= MCQQty − PurchaseQty − StockUsableQty |
| StockNonUsableAmount | DECIMAL(18,2) | \= Price × StockNonUsableQty |
| TotalExtraPaid | DECIMAL(18,2) | \= Diff × SurchargePaid |
| TotalExtraCollected | DECIMAL(18,2) | User input |

**3\. 🔗 API Integration**

**🧾 PO Number Search**

- **Endpoint**: GET /Api/GetPoes?pono={query}
- **Usage**: Autocomplete PO Number field
- **Response**: List of PO numbers

**📦 PO Detail Fetch**

- **Endpoint**: GET /Api/GetPoH2HDetail?pono={selectedPO}
- **Usage**: Populate MemoDetail rows
- **Response**: JSON array with:
    - Article
    - Color
    - Size
    - Rate (Price)
    - UOM (Unit)

**4\. 🖥️ UI Flow**

Memo List Contain function to Create new Memo and show existing memo list.
Create New Memo button used for create new fresh Memo will navigate to Form Creation which user will fill Header Form and Detail Form.
Detail Form show based on detail of PO. In Memo Detail after select Po Number, Item Po will be listed all, but there is function to filter and give check box, only required item in memo will be checked and only that item are saved to the memo.

**📋 Memo List Form**
- Data grid of memo list with filter, paging, search
- There is Create Memo Button out of grid
- There are Edit, Delete, Print button in each of rows.

- **📋 Memo Header Form**
- Auto-generated: Memo No (MO000001, MO000002, …)
- Dropdowns: Buyer, Style, Supplier
- Date Picker: Memo Date
- Autocomplete: PO Number (via API)

**📄 Memo Detail Grid**
- Auto-fill from PO Detail API
- Editable fields: BOMQty, AvailStockQty, MCQQty, SurchargePaid, StockUsableQty, TotalExtraCollected
- Calculated fields: All formulas auto-update on change
- Memo detail grid should be horizontal/vertical scrollable if not able to show all column one page

**5\. 🧮 Formula Logic**

|     |     |
| --- | --- |
| **Field** | **Formula** |
| BOMAmount | Price × BOMQty |
| StockAmount | Price × AvailStockQty |
| PurchaseQty | BOMQty − AvailStockQty |
| PurchaseAmount | Price × PurchaseQty |
| MCQAmount | Price × MCQQty |
| Diff | PurchaseAmount − MCQAmount |
| StockFromMCQ | MCQQty − PurchaseQty |
| StockUsableAmount | Price × StockUsableQty |
| StockNonUsableQty | MCQQty − PurchaseQty − StockUsableQty |
| StockNonUsableAmount | Price × StockNonUsableQty |
| TotalExtraPaid | Diff × SurchargePaid |

**6\. 🧱 Architecture Notes**

- **Entity Framework Core** for ORM
- **Repository Pattern** for data access
- **AutoMapper** for DTO conversion
- **Validation**: FluentValidation or DataAnnotations
- **Security**: Role-based access, anti-forgery tokens
- **Deployment**: IIS or Docker container