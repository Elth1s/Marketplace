import { Box, IconButton, Typography, Paper } from '@mui/material'
import { FC } from 'react'

import LinkRouter from '../LinkRouter'
import { BoxStyle, ImageBoxStyle } from './styled'

import { small_empty } from '../../assets/backgrounds'
import { FavoriteBorder } from '@mui/icons-material'

import { orange_heart, filled_orange_heart } from '../../assets/icons';
import { useActions } from '../../hooks/useActions'
import { useTranslation } from 'react-i18next'

interface Props {
    isSelected: boolean,
    name: string,
    image: string,
    urlSlug: string,
    statusName: string,
    price: number,
}

const ProductItem: FC<Props> = ({ isSelected, name, image, statusName, urlSlug, price }) => {
    const { t } = useTranslation();

    const { ChangeIsSelectedProducts } = useActions();

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
                            style={{ width: "220px", height: "220px", objectFit: "contain" }}
                            src={image != "" ? image : small_empty}
                            alt="productImage"
                        />
                    </ImageBoxStyle>
                    <Paper elevation={0} sx={{ minHeight: "46px", maxHeight: "46px", overflow: "hidden" }}>
                        <Typography variant="h5" fontWeight="medium">
                            {name}
                        </Typography>
                    </Paper>
                    <Typography variant="h6" color="secondary" fontWeight="medium" py="10px">
                        {statusName}
                    </Typography>
                    <Typography variant="h5" sx={{ marginTop: "auto" }}>
                    </Typography>
                    <Typography variant="h5">
                        {price} {t("currency")}
                    </Typography>
                </BoxStyle>
            </LinkRouter>
            <IconButton
                color="primary"
                sx={{
                    borderRadius: "12px",
                    position: "absolute",
                    zIndex: 2,
                    bottom: "4%",
                    right: "3%"
                }}
                onClick={() => ChangeIsSelectedProducts(urlSlug)}
            >
                <img
                    style={{ width: "30px", height: "30px" }}
                    src={isSelected ? filled_orange_heart : orange_heart}
                    alt="icon"
                />
            </IconButton>
        </Box>
    )
}

export default ProductItem;