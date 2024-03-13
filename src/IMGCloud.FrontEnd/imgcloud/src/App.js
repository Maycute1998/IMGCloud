import Home from "./containers/home";
import Navigation from "./containers/navigation";
import "./App.scss";

function App() {
  return (
    <div className="App">
      <Navigation />
      <div className="container">
        <Home />
      </div>
    </div>
  );
}

export default App;
