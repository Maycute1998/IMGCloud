import * as React from "react";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Error, Ok, Success, TOKEN_KEY, USER_NAME } from "../../const/constant";
import { login, register } from "../../services/user-service";
import { LoadingOverlayContext } from "../../stores/context/loading-overlay/loading-overlay-context";
import { SnackbarContext } from "../../stores/context/snackbar-context";
import RecoverPassword from "./recover-password";
import "./welcome.scss";

const Welcome = () => {
  const [action, setAction] = useState("login");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [username, setUsername] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [isForgotPassword, setIsForgotPassword] = useState(false);
  const [isValid, setIsValid] = useState(false);
  const navigate = useNavigate();
  const { showLoading } = React.useContext(LoadingOverlayContext);
  const { handleShowAlert, handleMessage, handleSeverity } =
    React.useContext(SnackbarContext);
  
    const validatePassword = (inputPassword) => {
    // Check if password has at least 8 characters
    const hasMinimumLength = inputPassword.length >= 8;

    // Check if password contains at least one number
    const hasNumber = /\d/.test(inputPassword);

    // Check if password contains both uppercase and lowercase letters
    const hasUppercase = /[A-Z]/.test(inputPassword);
    const hasLowercase = /[a-z]/.test(inputPassword);

    setIsValid(hasMinimumLength && hasNumber && hasUppercase && hasLowercase);
  };
  const handleSignIn = async (action) => {
    checkUserNameAndPassword();

    if (username && password) {
      showLoading(true);
      if (action === "login") {
        const res = await login(username, password);
        if (res && res.data.isSucceeded) {
          localStorage.setItem(TOKEN_KEY, res.data.token);
          localStorage.setItem(USER_NAME, username);
          navigate("/");
          showAlert(Success, res.data.message, true);
        } else {
          showAlert(Error, res.data.message, true);
        }
        showLoading(false);
      } else {
        let res = await register(email, username, password);
        if (res.status === Ok) {
          localStorage.setItem(USER_NAME, res.data.context);
          await login(username, password).then((res) => {
            if (res.status === Ok) {
              localStorage.setItem(TOKEN_KEY, res.data.token);
              localStorage.setItem(USER_NAME, username);
            }
          });
          navigate("/setup");
          showLoading(false);
        }
      }
    }
  };
  const showAlert = (alertType, errorMsg, isShowAlert) => {
    if (errorMsg) {
      handleSeverity(alertType);
      handleMessage(errorMsg);
      handleShowAlert(isShowAlert);
    }
  };

  const checkUserNameAndPassword = () => {
    if (username.trim() === "" || password.trim() === "") {
      setErrorMessage("Please enter both username and password.");
    } else {
      setErrorMessage("");
    }
  };

  const handlePasswordChange = (e) => {
    const password = e.target.value;
    setPassword(password);
    validatePassword(password);
  };

  return (
    <>
      {isForgotPassword ? (
        <RecoverPassword />
      ) : (
        <div class="signin-container">
          <div class="form-container sign-in">
            <div class="form">
              <div class="logo">
                <img
                  class="logo-image"
                  src="/img/imgcloud-logo.png"
                  alt="logo"
                />
              </div>
              <div class="social-icons">
                <a href="#" class="icon">
                  <i class="fa-brands fa-google-plus-g"></i>
                </a>
                <a href="#" class="icon">
                  <i class="fa-brands fa-facebook-f"></i>
                </a>
              </div>
              <span>or use your email password</span>
              {action === "login" ? (
                <> </>
              ) : (
                <input
                  type="email"
                  placeholder="Email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
              )}
              <input
                type="text"
                placeholder="User name"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
              <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={handlePasswordChange}
              />
              {errorMessage && (
                <div style={{ color: "red" }}>{errorMessage}</div>
              )}
              {action !== "login" && !isValid && 
              <span style={{ color: "red" }}>Your password must have at least 8 characters and contain uppercase letters, lowercase letters and numbers.</span>}
              <a href="#" onClick={() => setIsForgotPassword(true)}>
                Forget Your Password?
              </a>
              <button
                type="submit"
                class="btn btn-primary"
                onClick={() => handleSignIn(action)}
              >
                {action === "login" ? "Sign In" : "Sign Up"}
              </button>
            </div>
          </div>
          <div class="toggle-container">
            <div class="toggle">
              {action === "login" ? (
                <div class="toggle-panel toggle-left">
                  <h1>Hello, Friend!</h1>
                  <p>
                    Register with your personal details to use all of site
                    features
                  </p>
                  <button
                    class="hidden"
                    id="register"
                    onClick={() => setAction("signup")}
                  >
                    {action === "login" ? "Sign Up" : "Sign in"}
                  </button>
                </div>
              ) : (
                <div class="toggle-panel toggle-left">
                  <h1>Welcome Back!</h1>
                  <p>Enter your personal details to use all of site features</p>
                  <button
                    class="hidden"
                    id="login"
                    onClick={() => setAction("login")}
                  >
                    {action === "login" ? "Sign Up" : "Sign in"}
                  </button>
                </div>
              )}
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export default Welcome;
