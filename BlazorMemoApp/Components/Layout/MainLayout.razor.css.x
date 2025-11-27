.page {
    display: flex;
    height: 100vh;
}

.sidebar {
    background-color: #2c3e50;
    color: white;
    width: 220px;
    transition: width .25s ease;
    overflow: hidden;
}

/* collapsed mode */
.page.collapsed .sidebar {
    width: 60px;
}

main {
    flex: 1;
    overflow-y: auto;
}

.top-row {
    height: 3.5rem;
    display: flex;
    align-items: center;
}

.navbar-brand {
    font-weight: bold;
}

.page {
    display: flex;
    height: 100vh;
}

/* Sidebar */
.sidebar {
    width: 220px;
    background-color: #2f4050;
    color: white;
    transition: width 0.25s;
    overflow: hidden;
}

/* Main body */
main {
    flex: 1;
    background-color: #f5f5f5;
    overflow: auto;
}

/* Jika collapsed */
.page.collapsed .sidebar {
    width: 60px;
}

/* Top bar */
.top-row {
    height: 56px;
    background-color: #ffffff;
    border-bottom: 1px solid #ddd;
}
