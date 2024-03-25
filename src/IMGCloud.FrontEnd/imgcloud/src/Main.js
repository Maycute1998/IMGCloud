import { Alert, Backdrop, CircularProgress, Snackbar } from "@mui/material";
import React from "react";
import "./index.css";
import { LoadingOverlayContext } from "./stores/context/loading-overlay/loading-overlay-context";
import { SnackbarContext } from "./stores/context/snackbar-context";


export default function Main() {
  const { isOpen, severity, message } = React.useContext(SnackbarContext);
  const { isLoading } = React.useContext(LoadingOverlayContext);

  return (
    <>
    <Backdrop
        sx={{ color: "#fff", zIndex: (theme) => theme.zIndex.drawer + 1 }}
        open={isLoading}
      >
        <CircularProgress color="inherit" />
      </Backdrop>
      <Snackbar open={isOpen} autoHideDuration={3000} anchorOrigin={{vertical: 'top', horizontal: 'center'}}>
        <Alert severity={severity} color={severity} sx={{ width: "100%" }}>
          {message}
        </Alert>
      </Snackbar>
    </>
  );
}
