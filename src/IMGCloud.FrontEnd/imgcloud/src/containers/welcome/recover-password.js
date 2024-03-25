import React, { useState } from "react";
import { forgotPassword } from "../../services/user-service";
import { LoadingOverlayContext } from "../../stores/context/loading-overlay/loading-overlay-context";

const RecoverPassword = () => {
  const [email, setEmail] = useState("");
  const { showLoading } = React.useContext(LoadingOverlayContext);

  const handleForgotPassword = async (email) => {
    try {
      showLoading(true);
      if (email) {
        await forgotPassword(email);
      }
      showLoading(false);
    } catch (error) {
      console.error('Error in handleForgotPassword:', error);
    }
  }
  return (
    <div className="password-change">
      <div class="logo">
        <img class="logo-image" src="/img/imgcloud-logo.png" alt="logo" />
      </div>
      <div class="email-box">
        <input
          type="text"
          class="search-bar"
          placeholder="Email here"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        ></input>
        <button
          class="search-button"
          onClick={() => handleForgotPassword(email)}
        >
          Search
        </button>
          </div>
    </div>
  );
};

export default RecoverPassword;
