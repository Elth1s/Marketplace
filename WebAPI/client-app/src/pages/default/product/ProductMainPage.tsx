import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Grid from "@mui/material/Grid";
import Rating from "@mui/material/Rating";
import Typography from "@mui/material/Typography";
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Divider from '@mui/material/Divider';

import CreditCardIcon from '@mui/icons-material/CreditCard';
import StarIcon from '@mui/icons-material/Star';

import { characteristic, image, images, product, reviews } from "./data";

const ProductMainPage = () => {
    return (
        <>
            <Grid container sx={{ mb: "80px" }}>
                <Grid item xs={4}>
                    <Box
                        component="img"
                        src={image}
                        width="520px"
                        height="520px"
                        alt="Paella dish" />
                    <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                        {images.map((img, index) => (
                            <Box
                                key={index}
                                component="img"
                                src={img}
                                alt="Paella dish"
                                width="120px"
                                height="120px"
                                sx={{ mr: "10px" }}
                            />
                        ))}
                    </Box>
                </Grid>
                <Grid item xs={4}>
                    <Box sx={{ display: "flex", flexDirection: "column", justifyContent: "center", alignItems: "flex-start", height: "100%", pl: "80px" }}>
                        <Typography variant="h1">230грн</Typography>
                        <Box sx={{ display: "flex" }}>
                            <Rating
                                value={4.5}
                                precision={0.5}
                                readOnly
                                emptyIcon={<StarIcon fontSize="inherit" />} />
                            <Typography>5(10)</Typography>
                        </Box>
                        
                        <Button variant="outlined" sx={{mt:"45px", mb:"35px"}}>Контакти продавця</Button>
                        <Button variant="contained">Купити</Button>
                    </Box>
                </Grid>
                <Grid item xs={4}>
                    <Typography variant="h4">Оплата</Typography>
                    <Card
                        sx={{
                            border: "1px solid #000",
                            borderRadius: "10px",
                            mt: "30px",
                            mb: "80px"
                        }}>
                        <CardContent>
                            <List>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Післяплата «Нова Пошта»" />
                                </ListItem>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Оплата за реквізитами" />
                                </ListItem>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Готівка" />
                                </ListItem>
                            </List>
                        </CardContent>
                    </Card>
                    <Typography variant="h4">Доставка</Typography>
                    <Card sx={{ border: "1px solid #000", borderRadius: "10px", mt: "30px" }}>
                        <CardContent>
                            <List>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Доставка «Нова Пошта»" />
                                </ListItem>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Доставка «Mall»" />
                                </ListItem>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Укрпошта" />
                                </ListItem>
                            </List>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>

            <Grid container>
                <Grid item xs={6}>
                    <Typography variant="h4" sx={{ mb: "40px" }}>Характеристики</Typography>
                    {characteristic.map((item, index) => (
                        <Box
                            key={index}
                            sx={{
                                position: "relative",
                                display: "flex",
                                justifyContent: "space-between",
                                mb: "20px",
                            }}>
                            <Typography
                                variant="h6"
                                sx={{
                                    background: "#FFF"
                                }}>
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
                            <Typography variant="h6" sx={{ background: "#FFF" }}>
                                {item.value}
                            </Typography>
                        </Box>
                    ))}
                </Grid>
                <Grid item xs={6}>
                    <Box sx={{ pl: "120px" }}>
                        <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", mb: "40px" }}>
                            <Typography variant="h4">Відгук</Typography>
                            <Button variant="contained">Додати відгук</Button>
                        </Box>
                        {reviews.map((item, index) => (
                            <Card key={index} sx={{ border: "1px solid #000", borderRadius: "10px", mb: "20px", p: "35px" }}>
                                <CardContent>
                                    <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                                        <Typography variant="h4">{item.name}</Typography>
                                        <Box>
                                            <Rating
                                                value={item.rating}
                                                precision={0.5}
                                                readOnly
                                                emptyIcon={<StarIcon fontSize="inherit" />} />
                                            <Typography variant="body1">{item.data}</Typography>
                                        </Box>
                                    </Box>
                                    <Typography variant="h6" sx={{ mt: "20px" }}>{item.decs}</Typography>
                                </CardContent>
                            </Card>
                        ))}
                    </Box>
                </Grid>
            </Grid>
        </>
    );
}

export default ProductMainPage;