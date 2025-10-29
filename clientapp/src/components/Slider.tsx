import { useState } from 'react';

const slides = [
  { img: '/images/hero-avangard.png', caption: 'Советский авангард, стиль и дым' },
  { img: '/images/catalog-bg.png', caption: 'Наши лучшие товары' },
  { img: '/images/paper-fibers.png', caption: 'Качество ощущается даже на фоне' },
];

export default function Slider() {
  const [index, setIndex] = useState(0);

  const next = () => setIndex((i) => (i + 1) % slides.length);
  const prev = () => setIndex((i) => (i - 1 + slides.length) % slides.length);

  return (
    <div className="max-w-xl mx-auto">
      <div className="relative">
        <img
          src={slides[index].img}
          alt={slides[index].caption}
          className="rounded-lg w-full h-64 object-cover shadow-lg"
        />
        <div className="absolute bottom-4 left-0 right-0 text-white text-lg bg-black/50 py-2 px-4 rounded-b-lg">
          {slides[index].caption}
        </div>
      </div>
      <div className="flex justify-center gap-4 mt-4">
        <button onClick={prev} className="bg-yellow-700 text-white px-4 py-2 rounded">←</button>
        <button onClick={next} className="bg-yellow-700 text-white px-4 py-2 rounded">→</button>
      </div>
    </div>
  );
}