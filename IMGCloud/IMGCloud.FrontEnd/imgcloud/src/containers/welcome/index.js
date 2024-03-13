import Alert from "@mui/material/Alert";
import { GoogleLogin, useGoogleLogin } from "@react-oauth/google";
import * as React from "react";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Ok, TOKEN_KEY, USER_NAME } from "../../const/constant";
import { register } from "../../services/user-service";

import "./welcome.scss";

// TODO remove, this demo shouldn't need to reset the theme.
const Welcome = () => {
  const [alert, setAlert] = useState({ isShow: true, message: null });
  const [action, setAction] = useState("login");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [username, setUsername] = useState("");
  const navigate = useNavigate();

  const handleSignIn = async (action) => {
    if (username || password) {
      if (action === "login") {
        await login(username, password).then((res) => {
          if (res.status === Ok) {
            let token = res.data.token;
            localStorage.setItem(TOKEN_KEY, token);
            localStorage.setItem(USER_NAME, username);
          }
        });
        navigate("/");
      } else {
        let res = await register(email, username, password);
        console.log(res);
      }
    }
    setAlert({ isShow: true });
  };

  const renderAlert = () => {
    return <Alert severity="error">{alert.message}</Alert>;
  };

  const login = useGoogleLogin({
    onSuccess: (credentialResponse) => console.log(credentialResponse),
  });

  return (
    <div class="signin-container">
      {renderAlert()}
      <div class="form-container sign-in">
        <div class="form">
          <div class="logo">
            <img class="logo-image" src="/img/imgcloud-logo.png" alt="logo" />
          </div>
          <div class="social-icons">
            <GoogleLogin
              onSuccess={(credentialResponse) => {
                console.log(credentialResponse);
              }}
              onError={() => {
                console.log("Login Failed");
              }}
            />
            ;
            <a href="#" class="icon" onClick={() => login()}>
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
            onChange={(e) => setPassword(e.target.value)}
          />
          <a href="#">Forget Your Password?</a>
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
                Register with your personal details to use all of site features
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
  );
};

export default Welcome;
