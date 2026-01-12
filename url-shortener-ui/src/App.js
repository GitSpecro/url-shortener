import CreateShortUrl from "./Components/CreateShortUrl";
import LinksTable from "./Components/LinksTable";

function App() {
  return (
    <div>
      <CreateShortUrl onCreated={() => window.location.reload()} />
      <LinksTable />
    </div>
  );
}

export default App;