import { useState } from "react";
import { LoadingOverlayContext } from "../context/loading-overlay/loading-overlay-context";
export const LoadingOverlayProvider = ({ children }) => {
  const [isLoading, setLoading] = useState(false);
  const showLoading = (isLoading) => {
    setLoading(isLoading);
  };
  return (
    <LoadingOverlayContext.Provider
      value={{ isLoading, showLoading }}
    >
      {children}
    </LoadingOverlayContext.Provider>
  );
};
