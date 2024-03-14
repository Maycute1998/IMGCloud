import React, { useEffect, useState } from "react";
import { Ok } from "../../const/constant";
import { getAllPosts } from "../../services/post-service";
import "./post.scss";
const Post = () => {
  const [posts, setPosts] = useState(null);

  async function fetchPost() {
    // You can await here
    const response = await getAllPosts();
    if (response.status === Ok) {
      return response.data;
    }
  }

  useEffect(() => {
    fetchPost()
      .then((res) => {
        if (res) {
          setPosts(res);
        }
      })
      .catch((error) => {
        // Handle errors
        console.error("Error fetching data:", error);
      });
  }, []);

  return (
    <>
      {posts &&
        posts.map((post) => {
          return (
            <div className="box" key={post.id}>
              <img
                className="image"
                alt=""
                src={post.postImages[0].imagePath}
              />
              <div className="user-img">
                <img
                  className="avatar"
                  alt=""
                  src={post.users.userDetails.photo}
                />
                <div>
                  <span className="user-name">{post.users.userName}</span>
                  <span className="caption">{post.caption}</span>
                </div>
              </div>
            </div>
          );
        })}
    </>
  );
};
export default Post;
