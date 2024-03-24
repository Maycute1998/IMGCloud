import React from "react";
import ReactDOM from "react-dom/client";
import Main from "./Main";
import "./index.css";
import reportWebVitals from "./reportWebVitals";
import { LoadingOverlayProvider } from "./stores/provider/loading-overlay-provider";
import { SnackbarProvider } from "./stores/provider/snackbar-provider";



const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <LoadingOverlayProvider>
    <SnackbarProvider>
      <Main />
    </SnackbarProvider>
  </LoadingOverlayProvider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
