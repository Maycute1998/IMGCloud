import React from "react";
import script from "../../const/intro-content";
import "./intro.scss";
const SearchIntro = () => {
  return (
    // <div className="intro">
    //   <div>
    //     <h1>{script.img_picture}</h1>
    //   </div>
    // </div>
    <>
      <div className="search-section">
        <div className="search-body">
          <div class="search-ideas">
            <div class="search-box">
              <input type="text" placeholder="Search for an idea" />
              <button type="button">Explore</button>
            </div>
            <div className="search-text">
              <h1>{script.discover}</h1>
              <p>{script.discover2}</p>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default SearchIntro;
