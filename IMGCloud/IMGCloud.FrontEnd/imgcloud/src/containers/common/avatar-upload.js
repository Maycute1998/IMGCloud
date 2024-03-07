import { Dialog, DialogActions, DialogContent } from "@mui/material";
import Button from "@mui/material/Button";
import DialogTitle from "@mui/material/DialogTitle";
import React, { useState } from "react";
import Avatar from "react-avatar-edit";
const AvatarUpload = (props) => {
  const { onCloseUploadAvatar, selectedValue, open, getPreviewValue } = props;

  const [src, setSrc] = useState("");
  const [preview, setPreview] = useState("");
  const onClose = () => {
    setPreview(null);
  };
  const onCrop = (view) => {
    setPreview(view);
  };

  const handleClose = () => {
    onCloseUploadAvatar(selectedValue);
    if (getPreviewValue) getPreviewValue(preview);
  };
  return (
    <div>
      <Dialog onClose={handleClose} open={open}>
        <DialogTitle>Upload your avatar</DialogTitle>
        <DialogContent>
          <Avatar
            width={300}
            height={300}
            onCrop={onCrop}
            onClose={onClose}
            src={src}
          />
          {/* <img src={preview} alt="Preview" /> */}
        </DialogContent>
        <DialogActions>
          <Button autoFocus onClick={handleClose}>
            Disagree
          </Button>
          <Button onClick={handleClose} autoFocus>
            Agree
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
};

export default AvatarUpload;
