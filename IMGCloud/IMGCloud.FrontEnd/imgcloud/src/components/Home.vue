<template>
  <div class="data-grid">
    <div>
      <img class="image" src="../assets/img (1).jpg" />
      <div class="user">
        <div class="user-img">
          <img class="avatar" src="../assets/user.jpg" />
          <span>It's me</span>
        </div>
      </div>
    </div>
    <div>
      <img class="image" src="../assets/img (2).jpg" />
      <div class="user">
        <div class="user-img">
          <img class="avatar" src="../assets/user.jpg" />
          <span>It's me</span>
        </div>
      </div>
    </div>
    <div>
      <img class="image" src="../assets/img (3).jpg" />
      <div class="user">
        <div class="user-img">
          <img class="avatar" src="../assets/user.jpg" />
          <span>It's me</span>
        </div>
      </div>
    </div>
    <div>
      <img class="image" src="../assets/img (4).jpg" />
      <div class="user">
        <div class="user-img">
          <img class="avatar" src="../assets/user.jpg" />
          <span>It's me</span>
        </div>
      </div>
    </div>
    <div>
      <img class="image" src="../assets/img (5).jpg" />
      <div class="user">
        <div class="user-img">
          <img class="avatar" src="../assets/user.jpg" />
          <span>It's me</span>
        </div>
      </div>
    </div>
    <div>
      <img class="image" src="../assets/img (6).jpg" />
      <div class="user">
        <div class="user-img">
          <img class="avatar" src="../assets/user.jpg" />
          <span>It's me</span>
        </div>
      </div>
    </div>
    <div>
      <img class="image" src="../assets/img (7).jpg" />
      <div class="user">
        <div class="user-img">
          <img class="avatar" src="../assets/user.jpg" />
          <span>It's me</span>
        </div>
      </div>
    </div>
    <div>
      <img class="image" src="../assets/img (8).jpg" />
      <div class="user">
        <div class="user-img">
          <img class="avatar" src="../assets/user.jpg" />
          <span>It's me</span>
        </div>
      </div>
    </div>
    <div>
      <img class="image" src="../assets/img (9).jpg" />
      <div class="user">
        <div class="user-img">
          <img class="avatar" src="../assets/user.jpg" />
          <span>It's me</span>
        </div>
      </div>
    </div>
    <div>
      <img class="image" src="../assets/img (10).jpg" />
      <div class="user">
        <div class="user-img">
          <img class="avatar" src="../assets/user.jpg" />
          <span>It's me</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "HelloWorld",
  props: {
    msg: String,
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
/* * {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
} */
@media screen and (min-width: 1216px) {
  .container:not(.is-max-desktop) {
    max-width: 2000px !important;
  }
}

.container {
  width: 2000px;
  margin: 20px auto;
  columns: 6;
  column-gap: 20px;
}
.container .box {
  width: 100%;
  margin-bottom: 15px !important;
  padding: 0 !important;
  break-inside: avoid;
}
.container .box img {
  max-width: 100%;
  border-radius: 20px;
}
.user-name {
  margin-left: 2%;
}

.user-img {
  display: flex;
  flex-direction: row;
  align-items: center;
}

.image {
  border-radius: 36px;
}

.avatar {
  border-radius: 50%;
  width: 30px;
}
@media (max-width: 1200px) {
  .container {
    width: calc(100% - 40px);
    columns: 6;
  }
}

@media (max-width: 1000px) {
  .container {
    width: calc(100% - 40px);
    columns: 4;
  }
}

@media (max-width: 768px) {
  .container {
    columns: 3;
  }
}

@media (max-width: 480px) {
  .container {
    columns: 2;
  }
}
</style>
