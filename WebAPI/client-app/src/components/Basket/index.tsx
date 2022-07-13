import { Close, ShoppingCartOutlined } from '@mui/icons-material';
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
import { useNavigate } from "react-router-dom";

import { useActions } from '../../hooks/useActions';
import { useTypedSelector } from '../../hooks/useTypedSelector';
import { getLocalAccessToken } from '../../http_comon';

import LinkRouter from '../LinkRouter';
import BasketItem from './BasketItem';

const Basket = () => {
    const { GetBasketItems } = useActions();
    const { basketItems } = useTypedSelector(state => state.basket);

    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);

    const navigate = useNavigate();

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
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <>
            <IconButton
                sx={{ borderRadius: '12px', p: 0.5 }}
                size="large"
                aria-label="search"
                color="primary"
                onClick={handleClick}
            >
                <Badge badgeContent={basketItems.length} color="secondary">
                    <ShoppingCartOutlined sx={{ fontSize: "35px" }} />
                </Badge>
            </IconButton>
            <Menu
                anchorEl={anchorEl}
                id="basket-menu"
                open={open}
                onClose={handleClose}
                PaperProps={{
                    elevation: 0,
                    sx: {
                        borderRadius: 3,
                        overflow: 'visible',
                        filter: 'drop-shadow(0 4px 8px rgba(0,0,0,0.5))',
                        mt: 0.5,
                        minWidth: "500px",
                        maxHeight: "450px",
                        px: 2,
                        '& .MuiAvatar-root': {
                            width: 32,
                            height: 32,
                            ml: -0.5,
                            mr: 1,
                        }
                    },
                }}
                transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
            >
                <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                    <Typography variant='h4' fontWeight="bold">
                        Basket
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
                <Paper elevation={0} sx={{ maxHeight: "330px", overflow: 'auto', '&::-webkit-scrollbar': { display: "none" } }} >
                    {basketItems.map((row, index) => {
                        return (
                            <>
                                <Divider sx={{ my: 1, background: "#77777" }} />
                                <BasketItem id={row.id} image={row.productImage} name={row.productName} price={row.productPrice} />
                            </>
                        );
                    })}
                </Paper>
                <LinkRouter underline="none" color="common.black" to="/ordering">
                    <Button color="secondary" variant="contained" sx={{ width: "100%", my: 1, mt: 2 }} >
                        Order
                    </Button>
                </LinkRouter>
            </Menu>
        </>
    )
}

export default Basket;