import React, { useEffect } from "react";
import { Ok } from "../../const/constant";
import { getAllCollections } from "../../services/post-service";
import { LoadingOverlayContext } from "../../stores/context/loading-overlay/loading-overlay-context";
import { SnackbarContext } from "../../stores/context/snackbar-context";
// Import Swiper React components
import { Swiper, SwiperSlide } from "swiper/react";

// Import Swiper styles
import "swiper/css";
import "swiper/css/autoplay";
import "swiper/css/pagination";

import "./collection.scss";

// import required modules
import { Pagination } from "swiper/modules";

export default function Collection() {
  const [collections, setCollections] = React.useState(null);
  const { showLoading } = React.useContext(LoadingOverlayContext);
  const { handleShowAlert, handleMessage, handleSeverity } =
    React.useContext(SnackbarContext);

  const handleAlert = (alertType, message, isShow) => {
    handleSeverity(alertType);
    handleMessage(`${message}`);
    handleShowAlert(isShow);
  };

  async function fetchCollections() {
    showLoading(true);
    try {
      const response = await getAllCollections();
      if (response.status === Ok) {
        return response.data;
      }
    } catch (error) {
      handleAlert(Error, error, true);
    }
  }

  useEffect(() => {
    fetchCollections()
      .then((res) => {
        if (res) {
          setCollections(res);
        }
      })
      .finally(() => {
        showLoading(false);
      });
  }, []);

  return (
    <>
      <Swiper
        slidesPerView={1}
        spaceBetween={10}
        pagination={{
          clickable: true,
        }}
        breakpoints={{
          640: {
            slidesPerView: 2,
            spaceBetween: 20,
          },
          768: {
            slidesPerView: 4,
            spaceBetween: 40,
          },
          1024: {
            slidesPerView: 5,
            spaceBetween: 50,
          },
        }}
        modules={[Pagination]}
        className="mySwiper"
      >
        {collections &&
          collections.map((collection, index) => {
            return (
              <SwiperSlide key={index}>
                <div className="item-container" key={index}>
                  <img
                    className="image-item"
                    src={collection.photo}
                    alt={collection.collectionName}
                  
                  />
                  <div class="overlay">
                    <div class="text">{collection.collectionName}</div>
                  </div>
                </div>
              </SwiperSlide>
            );
          })}
      </Swiper>
    </>
  );
}
