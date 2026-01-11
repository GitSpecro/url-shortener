import './App.css';
import { useState, useEffect } from 'react';

function App() {

  const [urls, setUrls] = useState([]);

  useEffect(() => {
  fetch("https://localhost:7299/api/urls")
    .then(res => res.json())
    .then(data => setUrls(data))
    .catch(err => console.error(err));
}, []);

  return (
    <div style={{ padding: "2rem" }}>
      <h1>URL Shortener</h1>

      <div>
        <input placeholder="Enter URL..." />
        <button>Shorten</button>
      </div>

      <hr />

      <div>
        <h2>Your Links</h2>
        <ul>
          {urls.map((u, i) => (
            <li key={i}>
              {u.shortCode} → {u.originalUrl} ({u.clickCount} clicks)
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}

export default App;
