import { useState } from 'react'

function CreateShortUrl({ onCreated }) {
  const [url, setUrl] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    const response = await fetch("https://localhost:7299/api/urls", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ originalUrl: url })
    });

    if (response.ok) {
      setUrl("");
      onCreated();
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h1>Create Short URL</h1>
      <input
        value={url}
        onChange={(e) => setUrl(e.target.value)}
        placeholder="https://example.com"
      />
      <button type="submit" id="create-btn">Create</button>
    </form>
  );
}

export default CreateShortUrl