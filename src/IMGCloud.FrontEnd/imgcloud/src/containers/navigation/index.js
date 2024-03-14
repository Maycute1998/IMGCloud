import CloudIcon from "@mui/icons-material/Cloud";
import Logout from "@mui/icons-material/Logout";
import PersonAdd from "@mui/icons-material/PersonAdd";
import Settings from "@mui/icons-material/Settings";
import Avatar from "@mui/material/Avatar";
import Badge from "@mui/material/Badge";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Divider from "@mui/material/Divider";
import IconButton from "@mui/material/IconButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import React, { useEffect, useState } from "react";
import { Ok, USER_NAME } from "../../const/constant";
import { getUserDetailsByName } from "../../services/user-service";
import "./nav.scss";

const Navigation = () => {
  const [anchorEl, setAnchorEl] = useState(null);
  const [userData, setUserData] = useState("");

  const open = Boolean(anchorEl);
  const userName = localStorage.getItem(USER_NAME);
  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleLogout = () => {
    localStorage.clear();
    window.location.reload();
  };

  async function fetchData() {
    try {
      const response = await getUserDetailsByName(userName);
      if (response.status === Ok) {
        return response.data;
      } else {
        throw new Error("Failed to fetch user data");
      }
    } catch (error) {
      console.error("Error fetching user data:", error);
      throw error;
    }
  }

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const userData = await fetchData();
        if (userData && userData.context) {
          setUserData(userData.context);
        }
      } catch (error) {
        console.error("Error fetching user data:", error);
      }
    };
  
    if (userName) {
      fetchUserData();
    }
  }, [userName]);

  return (
    <div>
      <div class="wrapper">
        <div class="navbar">
          <div class="navbar_left">
            <div class="logo">
              <img class="logo-image" src="/img/imgcloud-logo.png" alt="logo" />
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
            {userData ? (
              <>
                <div class="notifications">
                  <IconButton aria-label={"show 100 new notifications"}>
                    <Badge badgeContent={100} color="secondary">
                      <CloudIcon color="action" />
                    </Badge>
                  </IconButton>
                </div>
                <div></div>
                <div class="profile">
                  <Box
                    sx={{
                      display: "flex",
                      alignItems: "center",
                      textAlign: "center",
                    }}
                  >
                    <IconButton
                      onClick={handleClick}
                      size="small"
                      sx={{ ml: 2 }}
                      aria-controls={open ? "account-menu" : undefined}
                      aria-haspopup="true"
                      aria-expanded={open ? "true" : undefined}
                    >
                      <Avatar
                        sx={{ width: 32, height: 32 }}
                        src={userData.photo}
                      >
                        {userData.photo ? null : userData.fullName.charAt(0)}
                      </Avatar>
                    </IconButton>
                  </Box>
                  <Menu
                    anchorEl={anchorEl}
                    id="account-menu"
                    open={open}
                    onClose={handleClose}
                    onClick={handleClose}
                    PaperProps={{
                      elevation: 0,
                      sx: {
                        overflow: "visible",
                        filter: "drop-shadow(0px 2px 8px rgba(0,0,0,0.32))",
                        mt: 1.5,
                        "& .MuiAvatar-root": {
                          width: 32,
                          height: 32,
                          ml: -0.5,
                          mr: 1,
                        },
                        "&::before": {
                          content: '""',
                          display: "block",
                          position: "absolute",
                          top: 0,
                          right: 14,
                          width: 10,
                          height: 10,
                          bgcolor: "background.paper",
                          transform: "translateY(-50%) rotate(45deg)",
                          zIndex: 0,
                        },
                      },
                    }}
                    transformOrigin={{ horizontal: "right", vertical: "top" }}
                    anchorOrigin={{ horizontal: "right", vertical: "bottom" }}
                  >
                    <MenuItem onClick={handleClose}>
                      <Avatar /> HIHIHI
                    </MenuItem>
                    <MenuItem onClick={handleClose}>
                      <Avatar /> My account
                    </MenuItem>
                    <Divider />
                    <MenuItem onClick={handleClose}>
                      <ListItemIcon>
                        <PersonAdd fontSize="small" />
                      </ListItemIcon>
                      Add another account
                    </MenuItem>
                    <MenuItem onClick={handleClose}>
                      <ListItemIcon>
                        <Settings fontSize="small" />
                      </ListItemIcon>
                      Settings
                    </MenuItem>
                    <MenuItem onClick={handleLogout}>
                      <ListItemIcon>
                        <Logout fontSize="small" />
                      </ListItemIcon>
                      Logout
                    </MenuItem>
                  </Menu>
                </div>
              </>
            ) : (
              <div className="signin">
                <Button href="/welcome">Log In</Button>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Navigation;
