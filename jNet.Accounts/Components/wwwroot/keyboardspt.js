
export function scrollIntoView(ele, alignToTop) {
    ele.scrollIntoView(alignToTop);
}

export function scrollIntoView2(ele) {
    ele.scrollIntoView();
}

export function addKeyHandler(ele, callback) {
    ele.addEventListener("keydown", function (e) {
        var modifiers = 0;
        if (e.altKey) modifiers += 1;
        if (e.shiftKey) modifiers += 2;
        if (e.ctrlKey) modifiers += 4;
        if (e.metaKey) modifiers += 8;
        var handled = callback.invokeMethod('OnKeyDownInt', e.key, modifiers);
        if (handled) {
            e.stopPropagation();
            e.preventDefault();
        }
    });
}
