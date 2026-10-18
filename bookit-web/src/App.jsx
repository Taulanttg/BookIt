import { useEffect, useState } from 'react'

function App() {
  const [venues, setVenues] = useState([])

  useEffect(() => {
    fetch('https://localhost:7212/api/Venues')
      .then((response) => response.json())
      .then((data) => setVenues(data))
  }, [])

  return (
    <div>
      <h1>BookIt</h1>
      {venues.map((venue) => (
        <div key={venue.id}>
          <h3>{venue.name}</h3>
          <p>{venue.city} - {venue.address}</p>
        </div>
      ))}
    </div>
  )
}

export default App