<template>
  <div class="columns">
    <div class="column is-9">
      <h1 class="subtitle">Welcome to ImgStory</h1>
      <section>
        <b-field>
          <b-input placeholder="Email" type="email"></b-input>
        </b-field>

        <b-field>
          <b-input placeholder="Number" type="number" min="10" max="20">
          </b-input>
        </b-field>

        <b-field>
          <b-input
            placeholder="User handle (custom validation for only lowercase)"
            type="text"
            required
            validation-message="Only lowercase is allowed"
            pattern="[a-z]*"
          >
          </b-input>
        </b-field>

        <b-field>
          <b-input placeholder="URL" type="url"></b-input>
        </b-field>

        <b-field>
          <b-input
            type="textarea"
            minlength="10"
            maxlength="100"
            placeholder="Maxlength automatically counts characters"
          >
          </b-input>
        </b-field>
      </section>
    </div>
    <div class="column">Auto</div>
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
.sign-up {
  max-width: 560px;
  max-height: 756px;
  border-radius: 16px;
  background-color: #ffffff;
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