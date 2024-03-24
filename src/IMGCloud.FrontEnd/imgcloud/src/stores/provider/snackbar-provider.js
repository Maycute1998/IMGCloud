
import { useState } from "react";
import { SnackbarContext } from "../context/snackbar-context";
export const SnackbarProvider = ({ children }) => {
  const [isOpen, setOpen] = useState(false);
  const [message, setMessage] = useState(false);
  const [severity, setSeverity] = useState(false);

  const handleShowAlert = (isOpen) => {
    setOpen(isOpen);
  };

  const handleMessage = (message) => {
    setMessage(message);
  }

  const handleSeverity = (severity) => {
    setSeverity(severity);
  }

  return (
    <SnackbarContext.Provider value={{ isOpen, severity, message, handleShowAlert, handleMessage, handleSeverity}}>
      
      {children}
    </SnackbarContext.Provider>
  );
};
