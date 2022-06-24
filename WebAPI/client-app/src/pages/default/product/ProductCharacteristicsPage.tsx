import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Grid from "@mui/material/Grid";
import Rating from "@mui/material/Rating";
import Typography from "@mui/material/Typography";
import Divider from '@mui/material/Divider';

import StarIcon from '@mui/icons-material/Star';

import { characteristic, characteristic2, image, images, product, reviews } from "./data";

const ProductCharacteristicsPage = () => {
    return (
        <Grid container>
            <Grid item xs={8}>
                {characteristic.map((item, index) => (
                    <Box
                        key={index}
                        sx={{
                            display: "flex",
                            justifyContent: "space-between",
                            position: "relative",
                        }}>
                        <Typography sx={{ background: "#FFF" }}>
                            {item.name}
                        </Typography>
                        <Divider
                            sx={{
                                position: "absolute",
                                height: "1px",
                                width: "100%",
                                bottom: "6px",
                                borderColor: "#000000",
                                borderStyle: "dotted",
                                borderBottomWidth: "revert",
                                zIndex: "-1",
                            }}
                        />
                        <Typography sx={{ background: "#FFF" }}>
                            {item.value}
                        </Typography>
                    </Box>
                ))}
            </Grid>
            <Grid item xs={4}>
                <Box
                    component="img"
                    width="320px"
                    height="320px"
                    src={image}
                    alt="Paella dish" />
                <Box sx={{ width: "115px" }}>
                    <div style={{ display: "flex", flexDirection: "row" }}>
                        {images.map((img, index) => (
                            <Box
                                key={index}
                                component="img"
                                width="50px"
                                height="50px"
                                src={img}
                                sx={{ mr: "10px" }} />
                        ))}
                    </div>
                </Box>
                <Typography>230грн</Typography>
                <Box sx={{ display: "flex" }}>
                    <Rating
                        value={4.5}
                        precision={0.5}
                        readOnly
                        emptyIcon={<StarIcon fontSize="inherit" />} />
                    <Typography>5(10)</Typography>
                </Box>

                <Button variant="contained">Купити</Button>
                <Button variant="outlined">Контакти продавця</Button>
            </Grid>
        </Grid>
    );
}

export default ProductCharacteristicsPage;