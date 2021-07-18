
export function startCapture(ele, pId) {
    ele.setPointerCapture(pId);
    return this.getSize(ele);
}

export function stopCapture(ele, pId) {
    ele.releasePointerCapture(pId);
}

export function getSize(ele) {
    return {
        top: ele.offsetTop,
        left: ele.offsetLeft,
        height: ele.clientHeight,
        width: ele.clientWidth,
    };
}

export function getParentSize(ele) {
    return this.getSize(ele.parentElement);
}
