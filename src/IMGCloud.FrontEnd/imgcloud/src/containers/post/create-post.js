import { Divider, Switch } from "@mui/material";
import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import React, { useState } from "react";
const CreatePost = () => {
  const [preview, setPreview] = useState("");

  const handleImageChange = (event) => {
    const file = event.target.files[0];

    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        setPreview(reader.result);
      };
      reader.readAsDataURL(file);
    }
  };
  return (
    <>
      <div className="new-post">
        <Box sx={{ flexGrow: 1 }}>
          <Grid container spacing={2}>
            <Grid xs={6}>
              <div>
                <label for="fileInput">Choose an image</label>
                {preview && (
                  <div>
                    <img
                      src={preview}
                      alt="Preview"
                      style={{ maxWidth: "100%", maxHeight: "300px" }}
                    />
                  </div>
                )}
                <input
                  type="file"
                  id="fileInput"
                  accept="image/*"
                  onChange={handleImageChange}
                />
              </div>
            </Grid>
            <Grid xs={6}>
              <input type="text" placeholder="How do you feel today?" />
              <textarea rows="4" placeholder="What's on your mind?" />
              <input type="text" placeholder="Where are you right now? " />
              <input type="text" placeholder="Change your privacy settings" />
              <input type="text" placeholder="Add this story to your collection" />
              <Divider />
              <Switch defaultChecked color="default" />
            </Grid>
          </Grid>
        </Box>
      </div>
    </>
  );
};

export default CreatePost;
