import React, { useEffect, useState } from "react";
import { Error, Ok } from "../../const/constant";
import { getAllPosts } from "../../services/post-service";
import { LoadingOverlayContext } from "../../stores/context/loading-overlay/loading-overlay-context";
import { SnackbarContext } from "../../stores/context/snackbar-context";
import "./post.scss";
const Post = () => {
  const [posts, setPosts] = useState(null);
  const { showLoading } = React.useContext(LoadingOverlayContext);
  const { handleShowAlert, handleMessage, handleSeverity } =
    React.useContext(SnackbarContext);

  async function fetchPost() {
    try {
      const response = await getAllPosts();
      if (response.status === Ok) {
        return response.data;
      }
    } catch (error) {
      handleSeverity(Error);
      handleMessage(`${error}`);
      handleShowAlert(true);
    }
  }

  useEffect(() => {
    showLoading(true);
    fetchPost()
      .then((res) => {
        if (res) {
          setPosts(res);
        }
      })
      .finally(() => {
        showLoading(false);
      });
  }, []);

  function handleClickImage() {}

  return (
    <>
      <div className="container">
        {posts &&
          posts.map((post) => {
            return (
              <div className="post">
                <div className="box" key={post.id}>
                  <img
                    className="image"
                    alt=""
                    src={post.imagePath}
                    onClick={handleClickImage()}
                  />
                  <div className="user-img">
                    <img
                      className="avatar"
                      alt={post.caption}
                      src={post.userAvatar}
                    />
                    <div className="user-details">
                      <span className="user-name">{post.userName}</span>
                      <span className="caption">{post.caption}</span>
                    </div>
                  </div>
                </div>
              </div>
            );
          })}
      </div>
    </>
  );
};
export default Post;
