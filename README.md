# KT_16_Records_PatternMatching

Вариант 1. Геометрические фигуры через записи
1) public record Circle(Point Center, double Radius);
2) public record Rectangle(Point TopLeft, Point BottomRight);
3) public record Rectangle(Point TopLeft, Point BottomRight);
4) Метод string Classify(object shape) через switch-выражение:
Circle { Center: { X: 0, Y: 0 } } (вложенный паттерн) → "окружность в начале координат".
Circle { Radius: 0 } → "вырожденная окружность (точка)".
Circle c → строка с радиусом.
Rectangle r when r.TopLeft == r.BottomRight → "вырожденный прямоугольник (точка)".
Rectangle r → строка с размерами.
_ → "неизвестная фигура".

# Результаты и проверочные ключи
<img width="1505" height="279" alt="изображение" src="https://github.com/user-attachments/assets/a91a83e9-af48-46f2-b0f0-fccc6e9a7903" />

# Результат который получился у меня:
<img width="1280" height="692" alt="изображение" src="https://github.com/user-attachments/assets/aa495cb9-2f32-4ed5-b505-cf6b23708bd5" />
<img width="1280" height="692" alt="изображение" src="https://github.com/user-attachments/assets/5625ca5e-9852-44fe-a751-8c00871f7e99" />

