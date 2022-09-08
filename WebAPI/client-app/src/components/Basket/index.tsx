import { Close } from '@mui/icons-material';
import {
    Menu,
    IconButton,
    Box,
    Typography,
    Badge,
    Divider,
    Paper,
    Button,
} from '@mui/material';
import React, { useEffect } from 'react'
import { useParams } from "react-router-dom";
import { useTranslation } from 'react-i18next';

import { useActions } from '../../hooks/useActions';
import { useTypedSelector } from '../../hooks/useTypedSelector';
import { getLocalAccessToken } from '../../http_comon';

import LinkRouter from '../LinkRouter';
import BasketItem from './BasketItem';

import { orange_shopping_cart, basket_empty } from '../../assets/icons';

const Basket = () => {
    const { t } = useTranslation();

    const { GetBasketItems, BasketMenuChange } = useActions();
    const { basketItems, isBasketMenuOpen } = useTypedSelector(state => state.basket);

    const anchorRef = React.useRef<HTMLButtonElement>(null);

    let { urlSlug } = useParams();

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        try {
            let token = getLocalAccessToken();
            if (token) {
                await GetBasketItems();
            }
        } catch (ex) {
        }
    };

    const handleClick = (event: any) => {
        BasketMenuChange()
    };

    const handleClose = () => {
        BasketMenuChange()
    };

    return (
        <>
            <IconButton
                ref={anchorRef}
                sx={{ borderRadius: '12px', p: 0.5 }}
                size="large"
                aria-label="search"
                color="primary"
                onClick={handleClick}
            >
                <Badge badgeContent={basketItems.length} color="secondary">
                    <img
                        style={{ width: "40px", height: "40px" }}
                        src={orange_shopping_cart}
                        alt="icon"
                    />
                </Badge>
            </IconButton>
            <Menu
                anchorEl={anchorRef.current}
                id="basket-menu"
                open={isBasketMenuOpen}
                onClose={handleClose}
                PaperProps={{
                    elevation: 0,
                    sx: {
                        borderRadius: "10px",
                        overflow: 'visible',
                        filter: 'drop-shadow(0 4px 8px rgba(0,0,0,0.5))',
                        pt: "20px",
                        minWidth: "500px",
                        maxHeight: "660px",
                        px: "20px",
                        pb: "25px",
                        mt: "20px",
                        '& .MuiAvatar-root': {
                            width: 32,
                            height: 32,
                            ml: -0.5,
                            mr: 1,
                        }
                    },
                }}
                MenuListProps={{
                    sx: {
                        p: 0
                    }
                }}
                transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
            >
                <Box sx={{ height: "auto", display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                    <Typography variant='h3' lineHeight="30px" fontWeight="bold">
                        {t('components.basket.title')}
                    </Typography>
                    <IconButton
                        aria-label="close"
                        onClick={handleClose}
                        sx={{ borderRadius: "12px" }}
                        color="inherit"
                    >
                        <Close />
                    </IconButton>
                </Box>
                <Paper elevation={0} sx={{ maxHeight: "480px", mt: "15px", overflow: 'auto', '&::-webkit-scrollbar': { display: "none" } }} >
                    {basketItems?.length != 0
                        ? basketItems.map((row, index) => {
                            return (
                                <Box key={`$basket_${row.id}`}>
                                    <Divider sx={{ background: "#77777" }} />
                                    <BasketItem
                                        id={row.id}
                                        count={row.count}
                                        image={row.productImage}
                                        name={row.productName}
                                        price={row.productPrice}
                                        productCount={row.productCount}
                                        urlSlug={row.productUrlSlug}
                                        closeBasket={handleClose}
                                        linkUrlSlug={urlSlug}
                                    />
                                </Box>
                            );
                        }) :
                        <Box sx={{ display: "flex", flexDirection: "column", alignItems: "center", my: "120px" }}>
                            <img
                                style={{ width: "100px", height: "100px" }}
                                src={basket_empty}
                                alt="basket_empty"
                            />
                            <Typography variant='h5' sx={{ my: "13.5px" }}>
                                {t("components.basket.basketEmpty")}
                            </Typography>
                            <Typography variant='subtitle1'>
                                {t("components.basket.basketEmptyDescription")}
                            </Typography>
                        </Box>
                    }
                </Paper>
                {basketItems?.length
                    ? <LinkRouter underline="none" to="/ordering" onClick={BasketMenuChange}>
                        <Button color="secondary" variant="contained" sx={{ width: "100%", mt: "15px", fontSize: "20px", lineHeight: "25px", py: "12.5px", textTransform: "none" }} >
                            {t('components.basket.order')}
                        </Button>
                    </LinkRouter>
                    : <LinkRouter underline="none" to="/catalog" onClick={handleClose}>
                        <Button color="secondary" variant="contained" sx={{ width: "100%", mt: "15px", fontSize: "20px", lineHeight: "25px", py: "12.5px", textTransform: "none" }} >
                            {t('components.basket.shopping')}
                        </Button>
                    </LinkRouter>
                }
            </Menu>
        </>
    )
}

export default Basket;