import Card from "@mui/material/Card"
import CardMedia from "@mui/material/CardMedia"
import CardContent from "@mui/material/CardContent";
import Box from "@mui/material/Box";

import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';

import { FC } from "react";
import { ICardProduct } from "./types";
import { CardContentStyle, CardPriceStyle, CardStatusStyle, CardTitleStyle } from "./styled";

import { small_empty } from "../../assets/backgrounds";

const CardProduct: FC<ICardProduct> = ({ image, name, statusName, price }) => {
    return (
        <Card sx={{ border: "1px solid #000", borderRadius: "10px", px: "20px", py: "25px" }}>
            <CardMedia
                component="img"
                width="260px"
                height="220px"
                image={image != "" ? image : small_empty}
                alt={name}
            />
            <CardContentStyle>
                <CardTitleStyle>{name}</CardTitleStyle>
                <CardStatusStyle>{statusName}</CardStatusStyle>
                <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                    <CardPriceStyle>{price}</CardPriceStyle>
                    <FavoriteBorderIcon />
                </Box>
            </CardContentStyle>
        </Card>
    );
}

export default CardProduct;