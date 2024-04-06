import React from "react";
import script from "../../const/intro-content";
import "./intro.scss";
const FirstIntro = () => {
  var paddingTop = 20;
  return (
    <>
      <div className="intro-section">
        <div className="intro-text">
          <h1>{script.img_picture}</h1>
          <h2>{script.img_picture2}</h2>
        </div>
        <div className="intro-img">
          <div class="recipe">
            <img
              src="https://storage.googleapis.com/imgcloud-storage/photos/img-9.jpg"
              alt="Recipe 1 Image"
              style={{ paddingTop: `${paddingTop-10}px` }}
            />
          </div>
          <div class="recipe">
            <img
              src="https://storage.googleapis.com/imgcloud-storage/photos/img-2.jpg"
              alt="Recipe 2 Image"
              style={{ paddingTop: `${paddingTop * 3}px` }}
            />
          </div>
          <div class="recipe">
            <img
              src="https://storage.googleapis.com/imgcloud-storage/photos/img-8.jpg"
              alt="Recipe 2 Image"
              style={{ paddingTop: `${paddingTop * 5}px` }}
            />
          </div>
          <div class="recipe">
            <img
              src="https://storage.googleapis.com/imgcloud-storage/photos/img-7.jpg"
              alt="Recipe 2 Image"
              style={{ paddingTop: `${paddingTop * 7}px` }}
            />
          </div>
          <div class="recipe">
            <img
              src="https://storage.googleapis.com/imgcloud-storage/photos/img-3.jpg"
              alt="Recipe 2 Image"
              style={{ paddingTop: `${paddingTop* 5}px` }}
            />
          </div>
          <div class="recipe">
            <img
              src="https://storage.googleapis.com/imgcloud-storage/photos/img-11.jpg"
              alt="Recipe 2 Image"
              style={{ paddingTop: `${paddingTop * 3}px` }}
            />
          </div>
          <div class="recipe">
            <img
              src="https://storage.googleapis.com/imgcloud-storage/photos/img-6.jpg"
              alt="Recipe 2 Image"
              style={{ paddingTop: `${paddingTop-10}px` }}
            />
          </div>
        </div>
      </div>
    </>
  );
};

export default FirstIntro;
