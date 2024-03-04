import React from "react";
import "./nav.scss";
import IconButton from "@mui/material/IconButton";
import Badge from "@mui/material/Badge";
import CloudIcon from "@mui/icons-material/Cloud";
class Navigation extends React.Component {
  constructor(props) {
    super(props);
    this.state = { anchorElNav: null, anchorElUser: null };
  }
  render() {
    function notificationsLabel(count) {
      if (count === 0) {
        return "no notifications";
      }
      if (count > 99) {
        return "more than 99 notifications";
      }
      return `${count} notifications`;
    }
    return (
      <div>
        <div class="wrapper">
          <div class="navbar">
            <div class="navbar_left">
              <div class="logo">
                <img
                  class="logo-image"
                  src="/img/imgcloud-logo.png"
                  alt="logo"
                />
              </div>
            </div>

            <div class="search-box">
              <input
                type="text"
                class="search-bar"
                placeholder="What are you searching for?"
              ></input>
            </div>

            <div class="navbar_right">
              <div class="notifications">
                <IconButton aria-label={notificationsLabel(100)}>
                  <Badge badgeContent={100} color="secondary">
                    <CloudIcon color="action" />
                  </Badge>
                </IconButton>
              </div>
              <div></div>
              <div class="profile">
                <div class="icon_wrap">
                  <img
                    class="user-avatar"
                    src="https://st.depositphotos.com/1005833/2249/i/950/depositphotos_22499805-stock-photo-portrait-of-young-beautiful-girl.jpg"
                    alt="profile_pic"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default Navigation;
