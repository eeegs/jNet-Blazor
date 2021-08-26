const mapbox = await fetch('_content/jNet.Mapbox/mapbox.js')
const mapboxtext = await mapbox.text()

export const MapBox = Function(`${mapboxtext}; return mapboxgl;`)()
