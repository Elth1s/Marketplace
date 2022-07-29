import { Box, IconButton, Typography } from '@mui/material'
import { FC } from 'react'

import LinkRouter from '../LinkRouter'
import { BoxStyle, ImageBoxStyle } from './styled'

import { empty } from '../../assets/backgrounds'
import { FavoriteBorder } from '@mui/icons-material'

import { orange_heart } from '../../assets/icons';

interface Props {
    name: string,
    image: string,
    urlSlug: string,
    statusName: string,
    price: number,
}

const ProductItem: FC<Props> = ({ name, image, statusName, urlSlug, price }) => {
    return (
        <Box sx={{ height: "415px", marginRight: "15px", marginBottom: "15px", position: "relative" }}>
            <LinkRouter underline="none" color="unset" to={`/product/${urlSlug}`}
                onClick={() => {
                    window.scrollTo({
                        top: 0,
                        behavior: 'smooth'
                    });
                }}>
                <BoxStyle>
                    <ImageBoxStyle>
                        <img
                            style={{ width: "250px", height: "240px", objectFit: "contain" }}
                            src={image != "" ? image : empty}
                            alt="productImage"
                        />
                    </ImageBoxStyle>
                    <Typography variant="h5">
                        {name}
                    </Typography>
                    <Typography variant="h6" color="secondary" fontWeight="medium" py="10px">
                        {statusName}
                    </Typography>
                    <Typography variant="h5">
                    </Typography>
                    <Typography variant="h5">
                        {price} &#8372;
                    </Typography>
                </BoxStyle>
            </LinkRouter>
            <IconButton color="primary" sx={{ borderRadius: "12px", position: "absolute", zIndex: 2, bottom: "5%", right: "5%" }}>
                <img
                    style={{ width: "30px", height: "30px" }}
                    src={orange_heart}
                    alt="icon"
                />
            </IconButton>
        </Box>
    )
}

export default ProductItem;