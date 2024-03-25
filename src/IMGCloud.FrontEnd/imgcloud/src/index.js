import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import App from "./App";
import Main from "./Main";
import Profile from "./containers/user-profile/profile";
import UserInfo from "./containers/userinfo";
import Welcome from "./containers/welcome";
import PasswordChange from "./containers/welcome/password-change";
import "./index.css";
import reportWebVitals from "./reportWebVitals";
import { LoadingOverlayProvider } from "./stores/provider/loading-overlay-provider";
import { SnackbarProvider } from "./stores/provider/snackbar-provider";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
  },
  {
    path: "welcome",
    element: <Welcome />,
  },
  {
    path: "setup",
    element: <UserInfo />,
  },
  {
    path: "resetpassword",
    element: <PasswordChange />,
  },
  {
    path: "profile",
    element: <Profile />,
  }
]);


const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <LoadingOverlayProvider>
    <SnackbarProvider>
      <Main />
      <RouterProvider router={router} />
    </SnackbarProvider>
  </LoadingOverlayProvider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
