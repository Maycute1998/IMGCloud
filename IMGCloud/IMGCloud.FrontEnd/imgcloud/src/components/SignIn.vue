<template>
  <div class="sign-in center">
    <h1 class="subtitle">Sign in</h1>
    <section>
      <b-field
        label="Email or mobile phone number"
        type="is-success"
        message="This username is available"
      >
        <b-input value="johnsilver" maxlength="30"></b-input>
      </b-field>

      <b-field label="Your password">
        <b-input type="password" value="iwantmytreasure" password-reveal>
        </b-input>
      </b-field>
      <b-button type="is-light" rounded expanded>Log in</b-button>
      <div class="privacy">
        By continuing, you agree to the Terms of use and Privacy Policy.
      </div>
      <div class="forgot-pw"><a href="#">Forgot your password?</a></div>
      <div class="divider">New to our community</div>
      <b-button rounded expanded>Create an account</b-button>
    </section>
  </div>
</template>

<script>
export default {
  data() {
    return {
      name: "John Silver",
    };
  },
};
</script>

<style>
main {
  background-image: url("./assets/background.jpg");
}
.sign-in {
  max-width: 560px;
  max-height: 756px;
  border-radius: 16px;
  background-color: #ffffff;
}
.center {
  margin: auto;
  width: 50%;
  padding: 25px;
  position: absolute;
  left: 50%;
  top: 50%;
  -webkit-transform: translate(-50%, 20%);
  transform: translate(-50%, 20%);
}
h1 {
  text-align: center;
}
label {
  font-weight: 100 !important;
  font-size: 16px;
}
h1.subtitle {
  font-size: 34px !important;
  font-weight: 200 !important;
  text-shadow: 1px 3px rgba(0, 0, 0, 0.25);
}
.button.is-rounded {
  font-weight: 500;
}
.privacy {
  padding: 20px 0px 20px 0px;
  font-size: 14px;
}
.forgot-pw {
  float: right;
  font-size: 12px;
  text-decoration: underline;
  transition: 0.4s;
}
.forgot-pw :focus {
  text-decoration: none;
}
.divider {
  font-size: 14px;
  display: flex;
  align-items: center;
  padding: 20px 0px 20px 0px;
  width: 100%;
}

.divider::before,
.divider::after {
  flex: 1;
  content: "";
  padding: 0.5px;
  background-color: rgba(0, 0, 0, 0.75);
  margin: 5px;
}
</style>