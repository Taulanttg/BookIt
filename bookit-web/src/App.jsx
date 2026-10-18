import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const [venues, setVenues] = useState([])
  const [events, setEvents] = useState([])

  useEffect(() => {
    fetch('https://localhost:7212/api/Venues')
      .then((response) => response.json())
      .then((data) => setVenues(data))

    fetch('https://localhost:7212/api/Events')
      .then((response) => response.json())
      .then((data) => setEvents(data))
  }, [])

  return (
    <div>
      <h1>BookIt</h1>

      <h2>Vendet</h2>
      <div className="grid">
        {venues.map((venue) => (
          <div className="card" key={venue.id}>
            <h3>{venue.name}</h3>
            <p>{venue.city} - {venue.address}</p>
            <p>{venue.description}</p>
          </div>
        ))}
      </div>

      <h2>Netë me muzikë live</h2>
      <div className="grid">
        {events.map((ev) => (
          <div className="card" key={ev.id}>
            <h3>{ev.name}</h3>
            <p>Artisti: {ev.artist}</p>
            <p>Data: {new Date(ev.startDate).toLocaleString()}</p>
            <p>Çmimi: {ev.price} €</p>
          </div>
        ))}
      </div>
    </div>
  )
}

export default App