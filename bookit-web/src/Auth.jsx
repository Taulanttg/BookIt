import { useState } from 'react'

const API = 'https://localhost:7212/api'

function Auth({ onLogin }) {
  const [mode, setMode] = useState('login')
  const [fullName, setFullName] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')

  async function handleSubmit(e) {
    e.preventDefault()
    setError('')

    if (mode === 'register') {
      const registerResponse = await fetch(`${API}/Auth/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ fullName, email, password }),
      })

      if (!registerResponse.ok) {
        setError(await registerResponse.text())
        return
      }
    }

    const loginResponse = await fetch(`${API}/Auth/login`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email, password }),
    })

    if (!loginResponse.ok) {
      setError(await loginResponse.text())
      return
    }

    const data = await loginResponse.json()
    onLogin(data)
  }

  return (
    <form className="form" onSubmit={handleSubmit}>
      <h3>{mode === 'login' ? 'Hyr në llogari' : 'Krijo llogari'}</h3>

      {mode === 'register' && (
        <input
          type="text"
          placeholder="Emri i plotë"
          value={fullName}
          onChange={(e) => setFullName(e.target.value)}
          required
        />
      )}

      <input
        type="email"
        placeholder="Email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
      />

      <input
        type="password"
        placeholder="Fjalëkalimi"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        required
      />

      {error && <p className="message">{error}</p>}

      <button type="submit">
        {mode === 'login' ? 'Hyr' : 'Regjistrohu'}
      </button>

      <button
        type="button"
        onClick={() => setMode(mode === 'login' ? 'register' : 'login')}
      >
        {mode === 'login' ? 'Nuk ke llogari? Regjistrohu' : 'Ke llogari? Hyr'}
      </button>
    </form>
  )
}

export default Auth