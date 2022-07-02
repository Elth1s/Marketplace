import Card from "@mui/material/Card"
import CardMedia from "@mui/material/CardMedia"
import CardContent from "@mui/material/CardContent";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";

import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';

import { FC } from "react";
import { ICardProduct } from "./types";

const CardProduct:FC<ICardProduct> = ({image, title, status, price}) => {
    return (
        <Card sx={{ border: "1px solid #000", borderRadius: "10px", mx: "25px" }}>
            <CardMedia
                component="img"
                width="220px"
                height="220px"
                image={image}
                alt={title}
                sx={{ pt: "20px", px: "16px" }}
            />
            <CardContent>
                <Typography variant="body1" sx={{ mb: "10px" }}>{title}</Typography>
                <Typography variant="body2" sx={{ color: "#0E7C3A", mb: "35px" }}>{status}</Typography>
                <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                    <Typography variant="h6">{price}</Typography>
                    <FavoriteBorderIcon />
                </Box>
            </CardContent>
        </Card>
    );
}

export default CardProduct;