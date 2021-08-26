
import { MapBox } from "./mapboxexport.js"

var mapStore = {};

function exec(id, func) {
    if (mapStore[id]) {
        func(mapStore[id]);
    }
}

export function setToken(token) {
    MapBox.accessToken = token;
}

export async function CreateMap(id, mapElement, startPos, zoom, style, callback) {
    var map = new MapBox.Map({
        container: mapElement, // container ID
        style: style, // style URL
        center: startPos, // starting position [lng, lat]
        zoom: zoom // starting zoom
    });

    map.on("load", function () {
        map.addControl(new mapboxgl.NavigationControl());
        callback.invokeMethodAsync("MapLoadedCallback");
    });

    mapStore[id] = map;
}

export function MapProject(id, lngLat) {
    exec(id, map => {
        return map.project(lngLat)
    });
}

export function MapUnproject(id, points) {
    exec(id, map => {
        return map.unproject(points)
    });
}

export function DeleteMap(id) {
    exec(id, map => {
        map.remove()
        delete mapStore[id];
    });
}

export function CreatePopup(id, options) {
    var p = new MapBox.Popup(options);
    mapStore[id] = p;
}

export function ShowPopup(id, mapId, lngLat) {
    exec(id, p => {
        p.setLngLat(lngLat);
        exec(mapId, map => p.addTo(map));
    });
}

export function UpdatePopup(id, element) {
    exec(id, p => {
        var copy = element.firstElementChild.cloneNode(true);
        p.setDOMContent(copy);
    });
}

export function HidePopup(id) {
    exec(id, p => p.remove());
}

export function DeletePopup(id) {
    exec(id, p => delete mapStore[id]);
}

export function CreateSource(mapid, id, data) {
    exec(mapid, map => map.addSource(id, data));
}

export function SetSource(mapid, id, data) {
    exec(mapid, map => map.getSource(id).setData(data));
}

export function DeleteSource(mapid, id) {
    exec(mapid, map => map.removeSource(id));
}

export function CreateLayer(mapid, data) {
    exec(mapid, map => map.addLayer(data));
}

export function RegisterLayerClicked(mapid, layerid, callback) {
    exec(mapid, map => {
        map.on("click", layerid, e => {
            if (e.features.length > 0) {
                callback.invokeMethodAsync("LayerClickedCallback", {
                    lngLat: e.lngLat.toArray(),
                    id: e.features[0].id,
                })
            }
        });

        map.on('mouseenter', layerid, () => {
            map.getCanvas().style.cursor = 'pointer';
        });

        map.on('mouseleave', layerid, () => {
            map.getCanvas().style.cursor = '';
        });
    });
}

export function DeleteLayer(mapid, id) {
    exec(mapid, map => {
        map.removeLayer(id)
    });
}
