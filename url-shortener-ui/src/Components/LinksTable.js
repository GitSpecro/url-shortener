import { useEffect, useState } from "react";

function LinksTable() {
  const [links, setLinks] = useState([]);

  const loadLinks = async () => {
    const response = await fetch("https://localhost:7299/api/urls");
    const data = await response.json();
    setLinks(data);
  };

  useEffect(() => {
    loadLinks();
  }, []);

  return (
    <div>
      <h2>My Links</h2>
      <button onClick={loadLinks}>Refresh</button>
      <table>
        <thead>
          <tr>
            <th>Short</th>
            <th>Original</th>
            <th>Clicks</th>
            <th>Wallet</th>
            <th>Share %</th>
          </tr>
        </thead>
        <tbody>
          {links.map((l) => (
            <tr key={l.shortCode}>
              <td><a href={`https://localhost:7299/${l.shortCode}`} target="_blank" rel="noopener noreferrer">https://localhost:7299/{l.shortCode}</a></td>
              <td>{l.originalUrl}</td>
              <td>{l.clickCount}</td>
              <td>R{l.walletBalance}</td>
              <td>{l.sharePercentage}%</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default LinksTable;