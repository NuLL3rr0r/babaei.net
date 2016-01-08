#ifndef __HELLOWORLD_SCENE_H__
#define __HELLOWORLD_SCENE_H__

#include "cocos2d.h"
/*****************************************
// Post-Processing Effects in Cocos2d-x //
+class PostProcess;
*****************************************/
class PostProcess;

class HelloWorld : public cocos2d::Layer
{
/*****************************************
// Post-Processing Effects in Cocos2d-x //
+private:
+	Layer* m_gameLayer;
+	PostProcess* m_colorPostProcessLayer;
+	PostProcess* m_grayPostProcessLayer;
*****************************************/
private:
	Layer* m_gameLayer;
	PostProcess* m_colorPostProcessLayer;
	PostProcess* m_grayPostProcessLayer;

public:
    static cocos2d::Scene* createScene();

    virtual bool init();
    
    // a selector callback
    void menuCloseCallback(cocos2d::Ref* pSender);
    
    // implement the "static create()" method manually
    CREATE_FUNC(HelloWorld);

/*****************************************
// Post-Processing Effects in Cocos2d-x //
+	virtual void update(float delta) override;
*****************************************/
	virtual void update(float delta) override;
};

#endif // __HELLOWORLD_SCENE_H__
