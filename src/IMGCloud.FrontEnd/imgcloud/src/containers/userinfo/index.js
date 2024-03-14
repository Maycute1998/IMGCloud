import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Step from "@mui/material/Step";
import StepLabel from "@mui/material/StepLabel";
import Stepper from "@mui/material/Stepper";
import Typography from "@mui/material/Typography";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Ok, USER_NAME } from "../../const/constant";
import { createUserInfo } from "../../services/user-service";
import AvatarUpload from "../common/avatar-upload";
import "./user-info.scss";

const steps = [
  "About you",
  "Upload profile picture",
  "What are you interested in?",
];

const UserInfo = () => {
  const [activeStep, setActiveStep] = React.useState(0);
  const [skipped, setSkipped] = React.useState(new Set());

  const [open, setOpen] = React.useState(false);
  const [selectedValue, setSelectedValue] = React.useState("");
  const [userInfo, setUserInfo] = useState({
    userName: localStorage.getItem(USER_NAME),
    fullName: "",
    address: "",
    birthday: "",
    imgSrc: "",
  });
  const [imgSrc, setImageSrc] = useState("");
  const navigate = useNavigate();

  const handleClickOpenUploadAvatar = () => {
    setOpen(true);
  };

  const handleCloseUploadAvatar = (value) => {
    setOpen(false);
    setSelectedValue(value);
  };

  const isStepOptional = (step) => {
    return step === 1;
  };

  const isStepSkipped = (step) => {
    return skipped.has(step);
  };

  const handleNext = () => {
    let newSkipped = skipped;
    if (activeStep === 2) {
      creatUserInfo(userInfo);
    }
    if (isStepSkipped(activeStep)) {
      newSkipped = new Set(newSkipped.values());
      newSkipped.delete(activeStep);
    }

    setActiveStep((prevActiveStep) => prevActiveStep + 1);
    setSkipped(newSkipped);
  };

  const handleBack = () => {
    setActiveStep((prevActiveStep) => prevActiveStep - 1);
  };

  const handleSkip = () => {
    if (!isStepOptional(activeStep)) {
      // You probably want to guard against something like this,
      // it should never occur unless someone's actively trying to break something.
      throw new Error("You can't skip a step that isn't optional.");
    }

    setActiveStep((prevActiveStep) => prevActiveStep + 1);
    setSkipped((prevSkipped) => {
      const newSkipped = new Set(prevSkipped.values());
      newSkipped.add(activeStep);
      return newSkipped;
    });
  };

  const handleReset = () => {
    setActiveStep(0);
  };

  const creatUserInfo = async () => {
    if (userInfo) {
      await createUserInfo(userInfo).then(async (res) => {
        if (res.status === Ok) {
          navigate("/");
        }
      });
    }
  };

  return (
    <div className="user-info">
      <div class="logo">
        <img class="logo-image" src="/img/imgcloud-logo.png" alt="logo" />
      </div>
      <Box sx={{ width: "100%" }}>
        <Stepper activeStep={activeStep}>
          {steps.map((label, index) => {
            const stepProps = {};
            const labelProps = {};
            if (isStepSkipped(index)) {
              stepProps.completed = false;
            }
            return (
              <Step key={label} {...stepProps}>
                <StepLabel {...labelProps}>{label}</StepLabel>
              </Step>
            );
          })}
        </Stepper>
        {activeStep === steps.length ? (
          <React.Fragment>
            <Typography sx={{ mt: 2, mb: 1 }}>
              All steps completed - you&apos;re finished
            </Typography>
            <Box sx={{ display: "flex", flexDirection: "row", pt: 2 }}>
              <Box sx={{ flex: "1 1 auto" }} />
              <Button onClick={handleReset}>Reset</Button>
            </Box>
          </React.Fragment>
        ) : (
          <React.Fragment>
            <Typography sx={{ mt: 2, mb: 1 }}>Step {activeStep + 1}</Typography>
            {activeStep === 0 && (
              <div className="user-form">
                <div class="form">
                  <input
                    type="text"
                    placeholder="Your (full) name"
                    value={userInfo.fullName}
                    onChange={(e) =>
                      setUserInfo({ ...userInfo, fullName: e.target.value })
                    }
                  />

                  <input
                    type="date"
                    placeholder="Your date of birth"
                    value={userInfo.birthday}
                    onChange={(e) =>
                      setUserInfo({ ...userInfo, birthday: e.target.value })
                    }
                  />

                  <input
                    type="text"
                    placeholder="You are living in..."
                    value={userInfo.address}
                    onChange={(e) =>
                      setUserInfo({ ...userInfo, address: e.target.value })
                    }
                  />
                  <input type="text" placeholder="Bio" />
                </div>
              </div>
            )}

            {activeStep === 1 && (
              <div className="user-form">
                <div className="user-upload-img">
                  <Button
                    variant="outlined"
                    onClick={handleClickOpenUploadAvatar}
                  >
                    Upload
                  </Button>
                  <AvatarUpload
                    selectedValue={selectedValue}
                    open={open}
                    onCloseUploadAvatar={handleCloseUploadAvatar}
                    getPreviewValue={(value) => setImageSrc(value)}
                  />
                  <img src={userInfo.imgSrc} alt="Preview" />
                </div>
              </div>
            )}

            <div className="steps">
              <Box sx={{ display: "flex", flexDirection: "row", pt: 2 }}>
                <Button
                  color="inherit"
                  disabled={activeStep === 0}
                  onClick={handleBack}
                  sx={{ mr: 1 }}
                >
                  Back
                </Button>
                <Box sx={{ flex: "1 1 auto" }} />
                {isStepOptional(activeStep) && (
                  <Button color="inherit" onClick={handleSkip} sx={{ mr: 1 }}>
                    Skip
                  </Button>
                )}

                <Button onClick={handleNext}>
                  {activeStep === steps.length - 1 ? "Finish" : "Next"}
                </Button>
              </Box>
            </div>
          </React.Fragment>
        )}
      </Box>
    </div>
  );
};

export default UserInfo;
