import { useEffect, useState } from 'react'
import './App.css'

const API = 'https://localhost:7212/api'

function App() {
  const [venues, setVenues] = useState([])
  const [events, setEvents] = useState([])
  const [tables, setTables] = useState([])

  const [selectedEvent, setSelectedEvent] = useState(null)
  const [selectedVenue, setSelectedVenue] = useState(null)

  const [customerName, setCustomerName] = useState('')
  const [customerPhone, setCustomerPhone] = useState('')
  const [guests, setGuests] = useState(1)
  const [tableId, setTableId] = useState('')
  const [reservationDate, setReservationDate] = useState('')
  const [message, setMessage] = useState('')

  useEffect(() => {
    fetch(`${API}/Venues`)
      .then((response) => response.json())
      .then((data) => setVenues(data))

    fetch(`${API}/Events`)
      .then((response) => response.json())
      .then((data) => setEvents(data))

    fetch(`${API}/Tables`)
      .then((response) => response.json())
      .then((data) => setTables(data))
  }, [])

  function resetForm() {
    setSelectedEvent(null)
    setSelectedVenue(null)
    setCustomerName('')
    setCustomerPhone('')
    setGuests(1)
    setTableId('')
    setReservationDate('')
  }

  async function sendReservation(body) {
    const response = await fetch(`${API}/Reservations`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
    })

    if (response.ok) {
      setMessage('Rezervimi u krye me sukses!')
      resetForm()
    } else {
      const errorText = await response.text()
      setMessage(errorText)
    }
  }

  function handleEventSubmit(e) {
    e.preventDefault()
    sendReservation({
      customerName: customerName,
      customerPhone: customerPhone,
      reservationDate: selectedEvent.startDate,
      guests: Number(guests),
      status: 'Pending',
      venueId: selectedEvent.venueId,
      eventId: selectedEvent.id,
    })
  }

  function handleTableSubmit(e) {
    e.preventDefault()
    sendReservation({
      customerName: customerName,
      customerPhone: customerPhone,
      reservationDate: reservationDate + ':00',
      guests: Number(guests),
      status: 'Pending',
      venueId: selectedVenue.id,
      tableId: Number(tableId),
    })
  }

  const venueTables = selectedVenue
    ? tables.filter((t) => t.venueId === selectedVenue.id)
    : []

    const minDate = new Date().toISOString().slice(0, 16)

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
            <button
              onClick={() => {
                resetForm()
                setSelectedVenue(venue)
                setMessage('')
              }}
            >
              Rezervo tavolinë
            </button>
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
            <button
              onClick={() => {
                resetForm()
                setSelectedEvent(ev)
                setMessage('')
              }}
            >
              Rezervo
            </button>
          </div>
        ))}
      </div>

      {selectedVenue && (
        <form className="form" onSubmit={handleTableSubmit}>
          <h3>Rezervo tavolinë: {selectedVenue.name}</h3>

          <select
            value={tableId}
            onChange={(e) => setTableId(e.target.value)}
            required
          >
            <option value="">Zgjidh tavolinën</option>
            {venueTables.map((t) => (
              <option key={t.id} value={t.id}>
                {t.name} ({t.capacity} vende)
              </option>
            ))}
          </select>

          <input
            type="datetime-local"
            min={minDate}
            max="2099-12-31T23:59"
            value={reservationDate}
            onChange={(e) => setReservationDate(e.target.value)}
            required
          />

          

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
          <button type="button" onClick={resetForm}>Anulo</button>
        </form>
      )}

      {selectedEvent && (
        <form className="form" onSubmit={handleEventSubmit}>
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
          <button type="button" onClick={resetForm}>Anulo</button>
        </form>
      )}
    </div>
  )
}

export default App