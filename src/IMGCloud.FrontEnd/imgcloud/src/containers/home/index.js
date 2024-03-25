import { PrintIcon, ShareIcon, SpeedDial, SpeedDialAction, SpeedDialIcon } from '@mui/icons-material';
import React from "react";
import Post from "../post";
import CreatePost from "../post/create-post";
import "./home.scss";
const actions = [
  { icon: <PrintIcon />, name: 'Print' },
  { icon: <ShareIcon />, name: 'Share' },
];
const Home = () => {
  const [isPost, setIsPost] = React.useState(false);
  return (
    <div>
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
