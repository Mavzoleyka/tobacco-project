const products = [
  { id: 1, name: "Marlboro Red", brand: "Marlboro" },
  { id: 2, name: "Camel Blue", brand: "Camel" },
];

const Catalog = () => (
  <div>
    <h1>Каталог товаров</h1>
    <div>
      {products.map(p => (
        <div key={p.id} style={{border: '1px solid #ccc', margin: 8, padding: 8}}>
          <h2>{p.name}</h2>
          <p>{p.brand}</p>
        </div>
      ))}
    </div>
  </div>
);
export default Catalog;