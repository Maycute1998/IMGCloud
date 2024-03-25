import React, { useState } from "react";
import { resetPassword } from "../../services/user-service";
import { LoadingOverlayContext } from "../../stores/context/loading-overlay/loading-overlay-context";

const PasswordChange = () => {
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const { showLoading } = React.useContext(LoadingOverlayContext);

  const getResetTokenFromUrl = () => {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get("token");
  };

  const handleChangePassword = async (confirmPassword) => {
    const token = getResetTokenFromUrl();
    if (password !== confirmPassword){
      alert('Passwords do not match')
      return;
    } 
    try{
      showLoading(true);
      const resetPassRequest = {
        newPassword: confirmPassword,
        token
        }
      await resetPassword(resetPassRequest);
      setPassword("");
      setConfirmPassword("");
      showLoading(false);
    }
    catch(error){
      console.error('Error in handleChangePassword:', error);
    }
  };
  return (
    <div className="password-change">
      <div class="logo">
        <img class="logo-image" src="/img/imgcloud-logo.png" alt="logo" />
      </div>
      <div class="email-box">
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <input
          type="password"
          placeholder="Password"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
        />
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
