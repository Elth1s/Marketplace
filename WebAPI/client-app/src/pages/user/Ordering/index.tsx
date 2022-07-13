import {
  Box,
  Button,
  Checkbox,
  CssBaseline,
  FormControl,
  FormControlLabel,
  Grid,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Typography,
  Container
} from "@mui/material";
import React from "react";
import logo from "../../../logo.svg";
import FmdGoodOutlinedIcon from "@mui/icons-material/FmdGoodOutlined";

const Ordering = () => {
  return (
    <Container sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
      <Box sx={{ mt: 2 }}>
        <img
          style={{ cursor: "pointer", width: "150px", height: "93px" }}
          src={logo}
          alt="logo"
        />
      </Box>
      <Typography sx={{ mt: 3, fontSize: 36 }}>
        Оформлення замовлення
      </Typography>
      <Box sx={{ mt: 3, mb: 7 }}>
        <Typography sx={{ fontSize: 27 }}>Контактні дані</Typography>
        <Grid sx={{ mt: 1 }} container spacing={7}>
          <Grid item xs={6} md={4}>
            <TextField
              sx={{ width: 430, height: 50, borderRadius: 15 }}
              label="Призвіще"
            />
          </Grid>
          <Grid item xs={6} md={4}>
            <TextField sx={{ width: 430, height: 50 }} label="Номер телефону" />
          </Grid>
          <Grid item xs={6} md={4}>
            <TextField
              sx={{ width: 440, height: 50 }}
              label="Промокод (необов'язково)"
            />
          </Grid>
          <Grid item xs={6} md={4}>
            <TextField sx={{ width: 430, height: 50 }} label="Ім'я" />
          </Grid>
          <Grid item xs={6} md={4}>
            <TextField sx={{ width: 430, height: 50 }} label="Email-адреса" />
          </Grid>
          <Grid item xs={6} md={4}>
            <TextField
              sx={{ width: 440, height: 50 }}
              label="Номер бонусної карти (необов'язково)"
            />
          </Grid>
        </Grid>
      </Box>
      <Box sx={{ mt: 3 }}>
        <Grid sx={{ mt: 4 }} container spacing={2}>
          <Grid item xs={6} md={8}>
            {" "}
            <Typography sx={{ mt: 3, fontSize: 36 }}>Ваше місто</Typography>
          </Grid>
          <Grid item xs={6} md={4}>
            {" "}
            <Typography sx={{ mt: 3, fontSize: 36, ml: 4 }}>Загалом</Typography>
          </Grid>
        </Grid>
        <Grid sx={{ mt: 4 }} container spacing={2}>
          <Grid item xs={6} md={8}>
            <Box sx={{ display: "flex" }}>
              <Box sx={{ height: 91, width: 268, display: "flex" }}>
                <FmdGoodOutlinedIcon sx={{ height: 30, width: 30, mt: 4 }} />
                <Box sx={{ ml: 4 }}>
                  <Typography sx={{ fontSize: 24 }}>Острог</Typography>
                  <Typography sx={{ fontSize: 24, mt: 3 }}>
                    Рівненська обл.
                  </Typography>
                </Box>
              </Box>
              <Box
                sx={{
                  height: 91,
                  width: 268,
                  display: "flex",
                  ml: 15,
                  justifyContent: "center",
                }}
              >
                <Button
                  sx={{ height: 80, width: 250, mt: 0.5, border: 4 }}
                  variant="outlined"
                  color="success"
                >
                  Змінити
                </Button>
              </Box>
            </Box>
          </Grid>
          <Grid item xs={6} md={4}>
            <Box sx={{ width: 460 }}>
              <Box
                sx={{ display: "flex", justifyContent: "space-between", ml: 4 }}
              >
                <Typography sx={{ fontSize: 20 }}>1 товар на суму:</Typography>
                <Typography sx={{ fontSize: 20 }}>293</Typography>
              </Box>
              <Box
                sx={{
                  display: "flex",
                  justifyContent: "space-between",
                  ml: 4,
                  mt: 4,
                }}
              >
                <Typography sx={{ fontSize: 20 }}>Разом до оплати:</Typography>
                <Typography sx={{ fontSize: 20 }}>293</Typography>
              </Box>
              <Box sx={{ display: "flex" }}>
                <Button
                  sx={{ height: 80, width: 440, mt: 4, ml: 4 }}
                  variant="contained"
                  color="success"
                >
                  Змінити
                </Button>
              </Box>
            </Box>
          </Grid>
        </Grid>
      </Box>
      <Box sx={{ mt: 10, mb: 10 }}>
        <Box sx={{ display: "flex" }}>
          <Typography sx={{ fontSize: 36 }}>Замовлення №1</Typography>
          <Typography sx={{ fontSize: 27, mt: 0.5, ml: 10 }}>
            На суму 293
          </Typography>
        </Box>
        <Box sx={{ mt: 5 }}>
          <Typography sx={{ fontSize: 27 }}>Товар продавця EUROSHOP</Typography>
        </Box>
        <Box sx={{ mt: 5, height: 200, width: 1153, display: "flex" }}>
          <img
            style={{ cursor: "pointer", width: "200px", height: "200px" }}
            src={logo}
            alt="logo"
          />
          <Box sx={{ width: 550 }}>
            <Box sx={{ ml: 10 }}>
              <Typography sx={{ fontSize: 20, mt: 9 }}>Намисто з коштовностями</Typography>
              <Typography sx={{ fontSize: 20, mt: 2 }}>Срібло</Typography>
            </Box>
          </Box>
          <Box sx={{ width: 250 }}>
            <Box sx={{ ml: 10 }}>
              <Typography sx={{ fontSize: 20, mt: 9 }}>Ціна</Typography>
              <Typography sx={{ fontSize: 20, mt: 2 }}>293</Typography>
            </Box>
          </Box>
          <Box sx={{ width: 200 }}>
            <Box sx={{ ml: 10 }}>
              <Typography sx={{ fontSize: 20, mt: 9 }}>Кількість</Typography>
              <Typography sx={{ fontSize: 20, mt: 2 }}>1</Typography>
            </Box>
          </Box>
        </Box>
      </Box>
      <Typography sx={{ mt: 3, fontSize: 36 }}>Спосіб доставки</Typography>
      <Box sx={{ mt: 3 }}>
        <FormControlLabel
          sx={{ fontSize: 27 }}
          control={<Checkbox />}
          label="Самовивіз"
        />
        <Box sx={{ mt: 3 }}>
          <Box sx={{ ml: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={6} md={4}>
                <FormControl sx={{ width: 430, height: 50, borderRadius: 15 }}>
                  <InputLabel id="select-label-post">Виберіть пошту</InputLabel>
                  <Select
                    labelId="select-label-post"
                    id="demo-simple-select"
                    label="Виберіть пошту"
                  >
                    <MenuItem value={10}>Ten</MenuItem>
                    <MenuItem value={20}>Twenty</MenuItem>
                    <MenuItem value={30}>Thirty</MenuItem>
                  </Select>
                </FormControl>
              </Grid>
              <Grid item xs={6} md={4}>
                <FormControl sx={{ width: 430, height: 50, borderRadius: 15 }}>
                  <InputLabel id="select-label-terminal">
                    Виберіть відділення
                  </InputLabel>
                  <Select
                    labelId="select-label-terminal"
                    id="demo-simple-select-0"
                    label="Виберіть відділення"
                  >
                    <MenuItem value={10}>Ten</MenuItem>
                    <MenuItem value={20}>Twenty</MenuItem>
                    <MenuItem value={30}>Thirty</MenuItem>
                  </Select>
                </FormControl>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Box>
      <Box sx={{ mt: 10 }}>
        <FormControlLabel
          sx={{ fontSize: 27 }}
          control={<Checkbox defaultChecked />}
          label="Кур'єр Укрпошта"
        />
        <Box sx={{ mt: 3 }}>
          <Box sx={{ ml: 3 }}>
            <Grid container spacing={6}>
              <Grid item xs={6} md={4}>
                <TextField sx={{ width: 380, height: 50 }} label="Вулиця" />
              </Grid>
              <Grid item xs={6} md={4}>
                <TextField sx={{ width: 380, height: 50 }} label="Дім" />
              </Grid>
              <Grid item xs={6} md={4}>
                <TextField sx={{ width: 380, height: 50 }} label="Квартира" />
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Box>
      <Box sx={{ mt: 3 }}>
        <FormControlLabel
          sx={{ fontSize: 27 }}
          control={<Checkbox />}
          label="Кур'єр Нова Пошта"
        />
        <Box sx={{ mt: 3 }}>
          <Box sx={{ ml: 3 }}>
            <Grid container spacing={6}>
              <Grid item xs={6} md={4}>
                <TextField sx={{ width: 380, height: 50 }} label="Вулиця" />
              </Grid>
              <Grid item xs={6} md={4}>
                <TextField sx={{ width: 380, height: 50 }} label="Дім" />
              </Grid>
              <Grid item xs={6} md={4}>
                <TextField sx={{ width: 380, height: 50 }} label="Квартира" />
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Box>
      <Box sx={{ mt: 3 }}>
        <FormControlLabel
          sx={{ fontSize: 27 }}
          control={<Checkbox />}
          label="Кур'єр Mall"
        />
        <Box sx={{ mt: 3 }}>
          <Box sx={{ ml: 3 }}>
            <Grid container spacing={6}>
              <Grid item xs={6} md={4}>
                <TextField sx={{ width: 380, height: 50 }} label="Вулиця" />
              </Grid>
              <Grid item xs={6} md={4}>
                <TextField sx={{ width: 380, height: 50 }} label="Дім" />
              </Grid>
              <Grid item xs={6} md={4}>
                <TextField sx={{ width: 380, height: 50 }} label="Квартира" />
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Box>
      <Box sx={{ mt: 10, mb: 10 }}>
        <Typography sx={{ fontSize: 27 }}>Оплата</Typography>
        <Box sx={{ mt: 3 }}>
          <FormControlLabel
            sx={{ fontSize: 27 }}
            control={<Checkbox defaultChecked />}
            label="Оплата при отриманні товару"
          />
        </Box>
      </Box>
    </Container>
  );
};

export default Ordering;
