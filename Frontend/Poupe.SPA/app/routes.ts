import { type RouteConfig, index, route } from "@react-router/dev/routes";

export default [
  index("routes/home.tsx"),
  route("categorias", "routes/category.tsx"),
  route("transacoes", "routes/transaction.tsx"),
] satisfies RouteConfig;
