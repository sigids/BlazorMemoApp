@inherits LayoutComponentBase

<div class="page @(isCollapsed ? "collapsed" : "expanded")">
    <div class="sidebar">
        <NavMenu />
    </div>

    <main>
        <div class="top-row px-4 d-flex align-items-center">
            <a class="navbar-brand" href="">BlazorMemoApp</a>

            <!-- Toggle Button -->
            <button class="btn btn-sm btn-secondary ms-3"
                    @onclick="ToggleSidebar">
                ☰
            </button>
        </div>

        <article class="content px-4">
            @Body
        </article>
    </main>
</div>

@code {
    bool isCollapsed = true;  // default collapsed

    void ToggleSidebar()
    {
        isCollapsed = !isCollapsed;
    }
}
