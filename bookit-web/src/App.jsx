import { useEffect, useState } from 'react'
import './App.css'

const API = 'https://localhost:7212/api'

function App() {
  const [venues, setVenues] = useState([])
  const [events, setEvents] = useState([])

  const [selectedEvent, setSelectedEvent] = useState(null)
  const [customerName, setCustomerName] = useState('')
  const [customerPhone, setCustomerPhone] = useState('')
  const [guests, setGuests] = useState(1)
  const [message, setMessage] = useState('')

  useEffect(() => {
    fetch(`${API}/Venues`)
      .then((response) => response.json())
      .then((data) => setVenues(data))

    fetch(`${API}/Events`)
      .then((response) => response.json())
      .then((data) => setEvents(data))
  }, [])

  async function handleSubmit(e) {
    e.preventDefault()

    const response = await fetch(`${API}/Reservations`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        customerName: customerName,
        customerPhone: customerPhone,
        reservationDate: selectedEvent.startDate,
        guests: Number(guests),
        status: 'Pending',
        venueId: selectedEvent.venueId,
        eventId: selectedEvent.id,
      }),
    })

    if (response.ok) {
      setMessage('Rezervimi u krye me sukses!')
      setSelectedEvent(null)
      setCustomerName('')
      setCustomerPhone('')
      setGuests(1)
    } else {
      const errorText = await response.text()
      setMessage(errorText)
    }
  }

  return (
    <div>
      <h1>BookIt</h1>

      {message && <p className="message">{message}</p>}

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
            <button onClick={() => { setSelectedEvent(ev); setMessage('') }}>
              Rezervo
            </button>
          </div>
        ))}
      </div>

      {selectedEvent && (
        <form className="form" onSubmit={handleSubmit}>
          <h3>Rezervo: {selectedEvent.name}</h3>

          <input
            type="text"
            placeholder="Emri"
            value={customerName}
            onChange={(e) => setCustomerName(e.target.value)}
            required
          />

          <input
            type="text"
            placeholder="Telefoni"
            value={customerPhone}
            onChange={(e) => setCustomerPhone(e.target.value)}
            required
          />

          <input
            type="number"
            min="1"
            value={guests}
            onChange={(e) => setGuests(e.target.value)}
            required
          />

          <button type="submit">Dërgo rezervimin</button>
          <button type="button" onClick={() => setSelectedEvent(null)}>
            Anulo
          </button>
        </form>
      )}
    </div>
  )
}

export default App