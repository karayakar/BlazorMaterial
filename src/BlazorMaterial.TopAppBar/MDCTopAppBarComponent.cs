using System;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Browser.Interop;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorMaterial
{
    public class MDCTopAppBarComponent : BlazorMaterialComponent
    {
        private const string ATTACH_FUNCTION = "BlazorMaterial.TopAppBar.AttachTo";
        private static readonly ClassBuilder<MDCTopAppBarComponent> _classNameBuilder;

        [Parameter]
        protected MDCTopAppBarStyle Style { get; set; }

        [Parameter]
        protected RenderFragment ChildContent { get; set; }

        protected string ClassString { get; set; }

        protected ElementRef _MDCTopAppBar;
        private bool _isFirstRender = true;

        static MDCTopAppBarComponent()
        {
            _classNameBuilder = new ClassBuilder<MDCTopAppBarComponent>("mdc", "top-app-bar")
                .DefineClass((c) => GetStyle(c.Style), c => true, PrefixSeparators.Modifier);
        }

        private static string GetStyle(MDCTopAppBarStyle style)
        {
            switch (style)
            {
                case MDCTopAppBarStyle.Fixed:
                    return "fixed";
                case MDCTopAppBarStyle.Prominent:
                    return "prominent";
                case MDCTopAppBarStyle.Short:
                    return "short";
                case MDCTopAppBarStyle.ShortCollapsed:
                    return "short-collapsed";
                default:
                    return string.Empty;
            }
        }

        protected override void OnInit()
        {
            this.ClassString = _classNameBuilder.Build(this, this.Class);
        }

        protected override void OnAfterRender()
        {
            if (this._isFirstRender)
            {
                this._isFirstRender = false;
                RegisteredFunction.Invoke<bool>(ATTACH_FUNCTION, this._MDCTopAppBar);
            }
        }
    }
}
