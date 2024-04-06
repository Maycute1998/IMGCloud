//import { ShareIcon, SpeedDial, SpeedDialAction, SpeedDialIcon } from '@mui/icons-material';
import ShareIcon from '@mui/icons-material/Share';
import SpeedDial from '@mui/material/SpeedDial';
import SpeedDialAction from '@mui/material/SpeedDialAction';
import SpeedDialIcon from '@mui/material/SpeedDialIcon';
import React from "react";
import Collection from '../collection';
import FirstIntro from '../intro/first-intro';
import SearchIntro from '../intro/search-intro';
import Post from "../post";
import CreatePost from "../post/create-post";
import "./home.scss";
const actions = [
  { icon: <ShareIcon />, name: 'Share' },
];
const Home = () => {
  const [isPost, setIsPost] = React.useState(false);
  return (
    <div>
      <FirstIntro/>
      <Collection/>
      <SearchIntro/>
      {isPost ? <CreatePost /> : <Post />}
      <SpeedDial
        ariaLabel="SpeedDial basic example"
        sx={{ position: 'fixed', bottom: 16, right: 16 }}
        icon={<SpeedDialIcon />}
      >
        {actions.map((action) => (
          <SpeedDialAction
            key={action.name}
            icon={action.icon}
            tooltipTitle={action.name}
            onClick={() => setIsPost(!isPost)}
          />
        ))}
      </SpeedDial>
    </div>
  );
}

export default Home;
