<template>
  <nav class="navbar">
    <div class="navbar-brand">
      <a class="navbar-item">
        <img
          src="https://raw.githubusercontent.com/buefy/buefy/dev/static/img/buefy-logo.png"
          alt="Buefy"
        />
      </a>

      <a class="navbar-item">Home</a>
      <a class="navbar-item">Discover</a>
    </div>

    <div class="navbar-menu">
      <div class="search-box">
        <section>
          <b-field>
            <b-autocomplete
              class="search-input"
              rounded
              v-model="name"
              :data="filteredDataArray"
              placeholder="e.g. jQuery"
              icon="magnify"
              clearable
              @select="(option) => (selected = option)"
            >
              <template #empty>No results found</template>
            </b-autocomplete>
          </b-field>
        </section>
      </div>
      <div class="navbar-end">
        <b-icon icon="cloud"></b-icon>

        <b-dropdown
          v-model="navigation"
          position="is-bottom-left"
          append-to-body
          aria-role="menu"
        >
          <template #trigger>
            <a class="navbar-item" role="button">
              <a class="navbar-item" role="button">
                <img class="avatar" src="../assets/user.jpg" />
              </a>
            </a>
          </template>
          <div class="user-img">
            <b-dropdown-item custom aria-role="menuitem">
              Logged as <b>May</b>
              <img class="avatar" src="../assets/user.jpg" />
            </b-dropdown-item>
          </div>

          <hr class="dropdown-divider" />
          <b-dropdown-item has-link aria-role="menuitem">
            <a href="https://google.com" target="_blank">
              <b-icon icon="link"></b-icon>
              Google (link)
            </a>
          </b-dropdown-item>
          <b-dropdown-item value="home" aria-role="menuitem">
            <b-icon icon="home"></b-icon>
            Home
          </b-dropdown-item>
          <b-dropdown-item value="products" aria-role="menuitem">
            <b-icon icon="cart"></b-icon>
            Products
          </b-dropdown-item>
          <b-dropdown-item value="blog" disabled aria-role="menuitem">
            <b-icon icon="book-open"></b-icon>
            Blog
          </b-dropdown-item>
          <hr class="dropdown-divider" aria-role="menuitem" />
          <b-dropdown-item value="settings">
            <b-icon icon="settings"></b-icon>
            Settings
          </b-dropdown-item>
          <b-dropdown-item value="logout" aria-role="menuitem">
            <b-icon icon="logout"></b-icon>
            Logout
          </b-dropdown-item>
        </b-dropdown>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  name: "HelloWorld",
  props: {
    msg: String,
  },
  data() {
    return {
      searchQuery: "",
      data: [
        "Angular",
        "Angular 2",
        "Aurelia",
        "Backbone",
        "Ember",
        "jQuery",
        "Meteor",
        "Node.js",
        "Polymer",
        "React",
        "RxJS",
        "Vue.js",
      ],
      name: "",
      selected: null,
    };
  },
  computed: {
    filteredDataArray() {
      return this.data.filter((option) => {
        return (
          option.toString().toLowerCase().indexOf(this.name.toLowerCase()) >= 0
        );
      });
    },
  },
  methods: {
    search() {
      this.$emit("search", this.searchQuery);
      this.searchQuery = "";
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}

.navbar-end,
.navbar-menu {
  display: flex;
  flex-direction: row;
  align-items: center;
}
.search-box {
  width: 100%;
  box-sizing: border-box;
}

.search-input {
  display: block;
  width: 100%;
  padding: 15px;

  color: #313131;
  font-size: 20px;

  appearance: none;
  border: none;
  outline: none;
  background: none;

  box-shadow: 0px 0px 16px rgba(0, 0, 0, 0.25);
  background-color: rgba(255, 255, 255, 0.5);
  border-radius: 0 16px 0 16px;
  transition: 0.4s;
}
</style>
