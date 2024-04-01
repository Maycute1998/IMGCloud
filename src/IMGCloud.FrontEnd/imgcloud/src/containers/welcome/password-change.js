import React, { useState } from "react";
import { resetPassword } from "../../services/user-service";
import { LoadingOverlayContext } from "../../stores/context/loading-overlay/loading-overlay-context";

const PasswordChange = () => {
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const { showLoading } = React.useContext(LoadingOverlayContext);
  const [errorMessage, setErrorMessage] = useState("");

  const getResetTokenFromUrl = () => {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get("token");
  };

  const handleChangePassword = async (confirmPassword) => {
    const token = getResetTokenFromUrl();
    if (password !== confirmPassword) {
      setErrorMessage("Please enter both username and password.");
      return;
    }
    try {
      showLoading(true);
      const resetPassRequest = {
        newPassword: confirmPassword,
        token,
      };
      await resetPassword(resetPassRequest);
      setPassword("");
      setConfirmPassword("");
      showLoading(false);
    } catch (error) {
      console.error("Error in handleChangePassword:", error);
    }
  };
  return (
    <div className="reset-password">
      <div class="logo">
        <img class="logo-image" src="/img/imgcloud-logo.png" alt="logo" />
      </div>
      <div class="password-box">
        <input
          type="password"
          class="password-input"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <input
          type="password"
          class="password-input"
          placeholder="Confirm password"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
        />
        {errorMessage && <div style={{ color: "red" }}>{errorMessage}</div>}
        <span>Your password must have at least 8 characters and contain uppercase letters, lowercase letters and numbers.</span>
        <div />
        <button
          class="search-button"
          onClick={() => handleChangePassword(confirmPassword)}
        >
          Search
        </button>
      </div>
    </div>
  );
};

export default PasswordChange;
