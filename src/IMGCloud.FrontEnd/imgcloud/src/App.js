import React from "react";
import "./App.scss";
import Home from "./containers/home";
import Navigation from "./containers/navigation";
function App() {
  return (
    <div className="App">
      <Navigation />
      
      <Home />
      
    </div>
  );
}

export default App;
